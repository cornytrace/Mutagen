using Loqui;
using Mutagen.Bethesda.Plugins.Binary.Overlay;
using Mutagen.Bethesda.Plugins.Binary.Streams;
using Noggog.Utility;
using System;
using System.Linq;

namespace Mutagen.Bethesda.Plugins.Binary.Translations
{
    public class LoquiBinaryOverlayTranslation<T>
    {
        public delegate T CreateFunc(
            OverlayStream stream,
            BinaryOverlayFactoryPackage package,
            RecordTypeConverter? recordTypeConverter);
        public static readonly CreateFunc Create = GetCreateFunc();

        private static CreateFunc GetCreateFunc()
        {
            var regis = LoquiRegistration.GetRegister(typeof(T));
            if (regis == null) throw new ArgumentException();
            var className = $"{regis.Namespace}.Internals.{regis.Name}BinaryOverlay";

            var tType = regis.ClassType.Assembly.GetType(className)!;
            var method = tType.GetMethods()
                .Where((methodInfo) => methodInfo.Name.Equals($"{regis.Name}Factory"))
                .Where((methodInfo) => methodInfo.IsStatic
                    && methodInfo.IsPublic)
                .Where((methodInfo) =>
                {
                    var param = methodInfo.GetParameters();
                    if (param.Length != 3) return false;
                    if (!param[0].ParameterType.Equals(typeof(OverlayStream))) return false;
                    if (!param[1].ParameterType.Equals(typeof(BinaryOverlayFactoryPackage))) return false;
                    if (!param[2].ParameterType.Equals(typeof(TypedParseParams?))) return false;
                    return true;
                })
                .FirstOrDefault();
            if (method != null)
            {
                return DelegateBuilder.BuildDelegate<CreateFunc>(method);
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
