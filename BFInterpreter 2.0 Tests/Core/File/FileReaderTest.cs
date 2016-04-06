using System.IO;
using BFInterpreter_2._0.Core.Exceptions;
using BFInterpreter_2._0.Core.File;
using Moq;
using NUnit.Framework;

namespace BFInterpreter_2._0_Tests.Core.File
{
    [TestFixture]
    public class FileReaderTest
    {
        [Test]
        public void Constructor_CheckForInvalidBrainfuckSourceFileExtension_ExpectInvalidFileExtensionException()
        {
            var fileProvider = Mock.Of<IFileProvider>(method => method.IsValidExtension() == false &&
                                                                method.Exists() == true);
            Assert.Throws<InvalidFileExtensionException>(delegate
            {
                var fileReader = new FileReader(fileProvider);
            });
        }
        [Test]
        public void ReadText_FilePath_ExpectFileNotFoundException()
        {
            var fileProvider = Mock.Of<IFileProvider>(method => method.Exists() == false);
            Assert.Throws<FileNotFoundException>(delegate
            {
                var fileReader = new FileReader(fileProvider);
            });
        }
    }
}
