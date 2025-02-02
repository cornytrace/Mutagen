using Loqui;
using Mutagen.Bethesda.Plugins.Records.Internals;
using Noggog;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Linq;
using System.Linq.Expressions;

namespace Mutagen.Bethesda.Plugins.Records
{
    /// <summary>
    /// A static class encapsulating the job of creating a new Mod in a generic context
    /// </summary>
    public static class ModInstantiator
    {
        private static Dictionary<GameCategory, ModInstantiator<IModGetter>.ImporterDelegate> _dict = new();

        static ModInstantiator()
        {
            foreach (var modRegistration in LoquiRegistration.StaticRegister.Registrations
                .WhereCastable<ILoquiRegistration, IModRegistration>())
            {
                _dict[modRegistration.GameCategory] = ModInstantiatorReflection.GetOverlay<IModGetter>(modRegistration);
            }
        }

        public static IModGetter Importer(ModPath path, GameRelease release, IFileSystem? fileSystem = null)
        {
            return _dict[release.ToCategory()](path, release, fileSystem);
        }
    }
    
    /// <summary>
    /// A static class encapsulating the job of creating a new Mod in a generic context
    /// </summary>
    /// <typeparam name="TMod">
    /// Type of Mod to instantiate.  Can be the direct class, or one of its interfaces.
    /// </typeparam>
    public static class ModInstantiator<TMod>
        where TMod : IModGetter
    {
        public delegate TMod ActivatorDelegate(ModKey modKey, GameRelease release);
        public delegate TMod ImporterDelegate(ModPath modKey, GameRelease release, IFileSystem? fileSystem = null);

        /// <summary>
        /// Function to call to retrieve a new Mod of type T
        /// </summary>
        public static readonly ActivatorDelegate Activator;

        /// <summary>
        /// Function to call to import a new Mod of type T
        /// </summary>
        public static readonly ImporterDelegate Importer;
        //
        // /// <summary>
        // /// Function to call to import a new Mod of type T
        // /// </summary>
        // public static readonly Func<ModPath, GameRelease, IFileSystem?, TMod> FileSystemImporter;

        static ModInstantiator()
        {
            if (!LoquiRegistration.TryGetRegister(typeof(TMod), out var regis))
            {
                throw new ArgumentException();
            }
            Activator = ModInstantiatorReflection.GetActivator<TMod>(regis);
            Importer = ModInstantiatorReflection.GetImporter<TMod>(regis);
        }
    }

    namespace Internals
    {
        public static class ModInstantiatorReflection
        {
            internal static ModInstantiator<TMod>.ActivatorDelegate GetActivator<TMod>(ILoquiRegistration regis)
                where TMod : IModGetter
            {
                var ctorInfo = regis.ClassType.GetConstructors()
                    .Where(c => c.GetParameters().Length >= 1)
                    .Where(c => c.GetParameters()[0].ParameterType == typeof(ModKey))
                    .First();
                var paramInfo = ctorInfo.GetParameters();
                ParameterExpression modKeyParam = Expression.Parameter(typeof(ModKey), "modKey");
                if (paramInfo.Length == 1)
                {
                    NewExpression newExp = Expression.New(ctorInfo, modKeyParam);
                    LambdaExpression lambda = Expression.Lambda(typeof(Func<ModKey, TMod>), newExp, modKeyParam);
                    var deleg = lambda.Compile();
                    return (ModKey modKey, GameRelease release) =>
                    {
                        return (TMod)deleg.DynamicInvoke(modKey)!;
                    };
                }
                else
                {
                    ParameterExpression releaseParam = Expression.Parameter(paramInfo[1].ParameterType, "release");
                    NewExpression newExp = Expression.New(ctorInfo, modKeyParam, releaseParam);
                    var funcType = Expression.GetFuncType(typeof(ModKey), paramInfo[1].ParameterType, typeof(TMod));
                    LambdaExpression lambda = Expression.Lambda(funcType, newExp, modKeyParam, releaseParam);
                    var deleg = lambda.Compile();
                    return (ModKey modKey, GameRelease release) =>
                    {
                        return (TMod)deleg.DynamicInvoke(modKey, (int)release)!;
                    };
                }
            }

            public static ModInstantiator<TMod>.ImporterDelegate GetImporter<TMod>(ILoquiRegistration regis)
                where TMod : IModGetter
            {
                if (regis.ClassType == typeof(TMod)
                    || regis.SetterType == typeof(TMod))
                {
                    var methodInfo = regis.ClassType.GetMethods()
                        .Where(m => m.Name == "CreateFromBinary")
                        .Where(c => c.GetParameters().Length >= 3)
                        .Where(c => c.GetParameters()[0].ParameterType == typeof(ModPath))
                        .First();
                    var paramInfo = methodInfo.GetParameters();
                    var paramExprs = paramInfo.Select(p => Expression.Parameter(p.ParameterType, p.Name)).ToArray();
                    MethodCallExpression callExp = Expression.Call(methodInfo, paramExprs);
                    var funcType = Expression.GetFuncType(paramInfo.Select(p => p.ParameterType).And(typeof(TMod)).ToArray());
                    LambdaExpression lambda = Expression.Lambda(funcType, callExp, paramExprs);
                    var deleg = lambda.Compile();
                    if (paramInfo[1].Name == "release")
                    {
                        return (ModPath modPath, GameRelease release, IFileSystem? fileSystem) =>
                        {
                            var args = new object?[paramInfo.Length];
                            args[0] = modPath;
                            args[1] = release;
                            args[^2] = true;
                            args[^1] = fileSystem;
                            return (TMod)deleg.DynamicInvoke(args)!;
                        };
                    }
                    else
                    {
                        return (ModPath modPath, GameRelease release, IFileSystem? fileSystem) =>
                        {
                            var args = new object?[paramInfo.Length];
                            args[0] = modPath;
                            args[^2] = true;
                            args[^1] = fileSystem;
                            return (TMod)deleg.DynamicInvoke(args)!;
                        };
                    }
                }
                else if (regis.GetterType == typeof(TMod))
                {
                    var overlayGet = GetOverlay<TMod>(regis);
                    return (ModPath modPath, GameRelease release, IFileSystem? fileSystem) =>
                    {
                        return overlayGet(modPath, release, fileSystem);
                    };
                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            public static ModInstantiator<TMod>.ImporterDelegate GetOverlay<TMod>(ILoquiRegistration regis)
                where TMod : IModGetter
            {
                var methodInfo = regis.ClassType.GetMethods()
                    .Where(m => m.Name == "CreateFromBinaryOverlay")
                    .Where(c => c.GetParameters().Length >= 1)
                    .Where(c => c.GetParameters()[0].ParameterType == typeof(ModPath))
                    .First();
                var paramInfo = methodInfo.GetParameters();
                var paramExprs = paramInfo.Select(p => Expression.Parameter(p.ParameterType, p.Name)).ToArray();
                MethodCallExpression callExp = Expression.Call(methodInfo, paramExprs);
                var funcType = Expression.GetFuncType(paramInfo.Select(p => p.ParameterType).And(regis.GetterType).ToArray());
                LambdaExpression lambda = Expression.Lambda(funcType, callExp, paramExprs);
                var deleg = lambda.Compile();
                if (paramInfo.Length > 1 && paramInfo[1].Name == "release")
                {
                    return (ModPath modPath, GameRelease release, IFileSystem? fileSystem) =>
                    {
                        var args = new object?[paramInfo.Length];
                        args[0] = modPath;
                        args[1] = release;
                        args[^1] = fileSystem;
                        return (TMod)deleg.DynamicInvoke(args)!;
                    };
                }
                else
                {
                    return (ModPath modPath, GameRelease release, IFileSystem? fileSystem) =>
                    {
                        var args = new object?[paramInfo.Length];
                        args[0] = modPath;
                        args[^1] = fileSystem;
                        return (TMod)deleg.DynamicInvoke(args)!;
                    };
                }
            }
        }
    }
}
