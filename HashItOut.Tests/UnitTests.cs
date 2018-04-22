using HashItOut.Services;
using HashItOut.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Linq;
using System.Text;

namespace HashItOut.Tests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void NoFileSelectedTest()
        {
            Mock<IFileService> fileService = new Mock<IFileService>();
            fileService.Setup(service => service.OpenFileDialog()).Returns((string)null);

            //Setup target and test
            HashViewModel target = new HashViewModel(fileService.Object);
            target.BrowseCommand.Execute(null);

            Assert.AreEqual(string.Empty, target.File.SelectedPath);
        }

        [TestMethod]
        public void LazyCogHashTest()
        {
            // Arrange.
            const string FILE_NAME = @"\\TESTS\lazycog.txt";
            const string FILE_CONTENTS = "The quick brown fox jumps over the lazy cog";

            Mock<IFileService> fileService = new Mock<IFileService>();
            fileService.Setup(service => service.OpenFileDialog()).Returns(FILE_NAME);
            fileService.Setup(service => service.OpenFile(FILE_NAME)).Returns(() => new MemoryStream(Encoding.ASCII.GetBytes(FILE_CONTENTS)));

            // Act.
            HashViewModel target = new HashViewModel(fileService.Object);
            target.BrowseCommand.Execute(null);

            // Assert.
            Assert.AreEqual(FILE_NAME, target.File.SelectedPath);

            Assert.AreEqual("1055d3e698d289f2af8663725127bd4b",
                target.Algorithms.First(x => x.Function == "MD5").ValueResult.ToLower(),
                "The MD5 hash result returns an unexpected value.");

            Assert.AreEqual("de9f2c7fd25e1b3afad3e85a0bd17d9b100db4b3",
                target.Algorithms.First(x => x.Function == "SHA-1").ValueResult.ToLower(),
                "The SHA-1 hash result returns an unexpected value.");
        }

        [TestMethod]
        public void LazyDogHashTest()
        {
            // Arrange.
            const string FILE_NAME = @"\\TESTS\lazydog.txt";
            const string FILE_CONTENTS = "The quick brown fox jumps over the lazy dog";

            Mock<IFileService> fileService = new Mock<IFileService>();
            fileService.Setup(service => service.OpenFileDialog()).Returns(FILE_NAME);
            fileService.Setup(service => service.OpenFile(FILE_NAME)).Returns(() => new MemoryStream(Encoding.ASCII.GetBytes(FILE_CONTENTS)));

            // Act.
            HashViewModel target = new HashViewModel(fileService.Object);
            target.BrowseCommand.Execute(null);

            // Assert.
            Assert.AreEqual(FILE_NAME, target.File.SelectedPath);

            Assert.AreEqual("9e107d9d372bb6826bd81d3542a419d6",
                target.Algorithms.First(x => x.Function == "MD5").ValueResult.ToLower(),
                "The MD5 hash result returns an unexpected value.");

            Assert.AreEqual("2fd4e1c67a2d28fced849ee1bb76e7391b93eb12",
                target.Algorithms.First(x => x.Function == "SHA-1").ValueResult.ToLower(),
                "The SHA-1 hash result returns an unexpected value.");
        }
    }
}
