using System.IO.Abstractions;
using Loqui;
using Mutagen.Bethesda.Oblivion;
using Mutagen.Bethesda.Oblivion.Internals;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Plugins.Records.Internals;
using Mutagen.Bethesda.Skyrim;
using Mutagen.Bethesda.Skyrim.Internals;
using Mutagen.Bethesda.Testing;
using Xunit;

namespace Mutagen.Bethesda.UnitTests.Plugins.Records
{
    public class NoReleaseModInstantiator_Test : AModInstantiator_Test<OblivionMod, IOblivionMod, IOblivionModGetter, OblivionModBinaryOverlay>
    {
        public override ModPath ModPath => TestDataPathing.OblivionTestMod;
        public override GameRelease Release => GameRelease.Oblivion;
        public override ILoquiRegistration Registration => OblivionMod_Registration.Instance;
    }

    public class ReleaseModInstantiator_Test : AModInstantiator_Test<SkyrimMod, ISkyrimMod, ISkyrimModGetter, SkyrimModBinaryOverlay>
    {
        public override ModPath ModPath => TestDataPathing.SkyrimTestMod;
        public override GameRelease Release => GameRelease.SkyrimSE;
        public override ILoquiRegistration Registration => SkyrimMod_Registration.Instance;
    }

    public abstract class AModInstantiator_Test<TDirect, TSetter, TGetter, TOverlay>
        where TDirect : IMod
        where TSetter : IMod
        where TGetter : IModGetter
        where TOverlay : IModGetter
    {
        public abstract ModPath ModPath { get; }
        public abstract GameRelease Release { get; }
        public abstract ILoquiRegistration Registration { get; }

        [Fact]
        public void Direct()
        {
            var ret = ModInstantiatorReflection.GetActivator<TDirect>(Registration)(ModPath, Release);
            Assert.IsType<TDirect>(ret);
            Assert.Equal(ModPath.ModKey, ret.ModKey);
        }

        [Fact]
        public void Setter()
        {
            var ret = ModInstantiatorReflection.GetActivator<TSetter>(Registration)(ModPath, Release);
            Assert.IsType<TDirect>(ret);
            Assert.Equal(ModPath.ModKey, ret.ModKey);
        }

        [Fact]
        public void Getter()
        {
            var ret = ModInstantiatorReflection.GetActivator<TGetter>(Registration)(ModPath, Release);
            Assert.IsType<TDirect>(ret);
            Assert.Equal(ModPath.ModKey, ret.ModKey);
        }
        [Fact]
        public void Import_Direct()
        {
            var ret = ModInstantiatorReflection.GetImporter<TDirect>(Registration)(
                ModPath,
                Release,
                default(IFileSystem?));
            Assert.IsType<TDirect>(ret);
            Assert.Equal(ModPath.ModKey, ret.ModKey);
        }

        [Fact]
        public void Import_Setter()
        {
            var ret = ModInstantiatorReflection.GetImporter<TSetter>(Registration)(
                ModPath,
                Release,
                default(IFileSystem?));
            Assert.IsType<TDirect>(ret);
            Assert.Equal(ModPath.ModKey, ret.ModKey);
        }

        [Fact]
        public void Import_Getter()
        {
            var ret = ModInstantiatorReflection.GetImporter<TGetter>(Registration)(
                ModPath,
                Release,
                default(IFileSystem?));
            Assert.IsType<TOverlay>(ret);
            Assert.Equal(ModPath.ModKey, ret.ModKey);
        }
    }
}
