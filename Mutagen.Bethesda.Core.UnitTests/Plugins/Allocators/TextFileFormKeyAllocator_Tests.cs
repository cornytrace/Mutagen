using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.IO.Abstractions.TestingHelpers;
using FluentAssertions;
using Mutagen.Bethesda.Core.UnitTests.AutoData;
using Mutagen.Bethesda.Plugins;
using Mutagen.Bethesda.Plugins.Allocators;
using Mutagen.Bethesda.Plugins.Records;
using Mutagen.Bethesda.Testing;
using Xunit;

namespace Mutagen.Bethesda.Core.UnitTests.Plugins.Allocators
{
    public class TextFileFormKeyAllocator_Tests : IPersistentFormKeyAllocator_Tests<TextFileFormKeyAllocator>
    {
        protected override TextFileFormKeyAllocator CreateAllocator(IFileSystem fileSystem, IMod mod, string path)
        {
            return new(mod, path, preload: true, fileSystem: fileSystem);
        }

        protected override string ConstructTypicalPath(IFileSystem fileSystem)
        {
            return "C:/SomeFile";
        }

        [Fact]
        public void StaticExport()
        {
            var fileSystem = new MockFileSystem();
            var someFile = "C:/SomeFile";
            TextFileFormKeyAllocator.WriteToFile(
                someFile,
                new KeyValuePair<string, FormKey>[]
                {
                    new KeyValuePair<string, FormKey>(TestConstants.Edid1, TestConstants.Form1),
                    new KeyValuePair<string, FormKey>(TestConstants.Edid2, TestConstants.Form2),
                },
                fileSystem);

            var lines = fileSystem.File.ReadAllLines(someFile);
            Assert.Equal(
                new string[]
                {
                    TestConstants.Edid1,
                    TestConstants.Form1.ID.ToString(),
                    TestConstants.Edid2,
                    TestConstants.Form2.ID.ToString(),
                },
                lines);
        }

        [Theory, MutagenAutoData]
        public void TypicalImport(IMod mod)
        {
            var fileSystem = new MockFileSystem();
            var someFile = "C:/SomeFile";
            fileSystem.File.WriteAllLines(
                someFile,
                new string[]
                {
                    TestConstants.Edid1,
                    TestConstants.Form1.ID.ToString(),
                    TestConstants.Edid2,
                    TestConstants.Form2.ID.ToString(),
                });
            using var allocator = new TextFileFormKeyAllocator(mod, someFile, fileSystem: fileSystem);
            var formID = allocator.GetNextFormKey(TestConstants.Edid1);
            Assert.Equal(mod.ModKey, formID.ModKey);
            Assert.Equal(formID.ID, TestConstants.Form1.ID);
            formID.ModKey.Should().Be(mod.ModKey);
            formID = allocator.GetNextFormKey(TestConstants.Edid2);
            Assert.Equal(formID.ID, TestConstants.Form2.ID);
            formID.ModKey.Should().Be(mod.ModKey);
        }

        [Theory, MutagenAutoData]
        public void FailedImportTruncatedFile(IMod mod)
        {
            var fileSystem = new MockFileSystem();
            var someFile = "C:/SomeFile";

            fileSystem.File.WriteAllLines(
                someFile,
                new string[]
                {
                    TestConstants.Edid1,
                    TestConstants.Form1.ID.ToString(),
                    TestConstants.Edid2,
                });
            Assert.Throws<ArgumentException>(() => new TextFileFormKeyAllocator(mod, someFile, preload: true, fileSystem:fileSystem));
        }

        [Theory, MutagenAutoData]
        public void FailedImportDuplicateFormKey(IMod mod)
        {
            var fileSystem = new MockFileSystem();
            var someFile = "C:/SomeFile";

            fileSystem.File.WriteAllLines(
                someFile,
                new string[]
                {
                    TestConstants.Edid1,
                    TestConstants.Form1.ID.ToString(),
                    TestConstants.Edid2,
                    TestConstants.Form1.ID.ToString(),
                });
            Assert.Throws<ArgumentException>(() => new TextFileFormKeyAllocator(mod, someFile, preload: true, fileSystem: fileSystem));
        }

        [Theory, MutagenAutoData]
        public void FailedImportDuplicateEditorId(IMod mod)
        {
            var someFile = "C:/SomeFile";

            var fileSystem = new MockFileSystem();
            fileSystem.File.WriteAllLines(
                someFile,
                new string[]
                {
                    TestConstants.Edid1,
                    TestConstants.Form1.ID.ToString(),
                    TestConstants.Edid1,
                    TestConstants.Form2.ID.ToString(),
                });
            Assert.Throws<ArgumentException>(() => new TextFileFormKeyAllocator(mod, someFile, preload: true, fileSystem: fileSystem));
        }

        [Theory, MutagenAutoData]
        public void TypicalReimport(IMod mod)
        {
            var fileSystem = new MockFileSystem();
            var someFile = "C:/SomeFile";
            TextFileFormKeyAllocator.WriteToFile(
                someFile,
                new KeyValuePair<string, FormKey>[]
                {
                    new KeyValuePair<string, FormKey>(TestConstants.Edid1, TestConstants.Form1),
                    new KeyValuePair<string, FormKey>(TestConstants.Edid2, TestConstants.Form2),
                },
                fileSystem);
            using var allocator = new TextFileFormKeyAllocator(mod, someFile, preload: true, fileSystem: fileSystem);
            var formID = allocator.GetNextFormKey(TestConstants.Edid1);
            Assert.Equal(mod.ModKey, formID.ModKey);
            Assert.Equal(formID.ID, TestConstants.Form1.ID);
            Assert.Equal(formID.ModKey, mod.ModKey);
            formID = allocator.GetNextFormKey(TestConstants.Edid2);
            Assert.Equal(formID.ID, TestConstants.Form2.ID);
            Assert.Equal(formID.ModKey, mod.ModKey);
        }
    }
}
