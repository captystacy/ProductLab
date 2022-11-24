using CardPullouter.Core.Tests.Helpers;

namespace CardPullouter.Core.Tests
{
    public class FileReaderTests
    {
        [Fact]
        public async Task ItShould_get_keys_from_file()
        {
            // arrange

            var sut = new FileReader();
            var fileName = "Keys.txt";
            var filePath = DirectoryHelper.GetPathToFileFromSourceFolder(fileName);

            // act

            var getKeysOperation = await sut.GetKeys(filePath);

            // assert

            Assert.True(getKeysOperation.Ok);
            Assert.NotNull(getKeysOperation.Result);
            Assert.Contains("Игрушки", getKeysOperation.Result);
            Assert.Contains("Настолки", getKeysOperation.Result);
            Assert.Contains("Телефоны", getKeysOperation.Result);
        }

        [Fact]
        public async Task ItShould_get_bad_result_when_file_is_empty()
        {
            // arrange

            var sut = new FileReader();
            var fileName = "KeysEmpty.txt";
            var filePath = DirectoryHelper.GetPathToFileFromSourceFolder(fileName);

            // act

            var getKeysOperation = await sut.GetKeys(filePath);

            // assert

            Assert.False(getKeysOperation.Ok);
        }

        [Fact]
        public async Task ItShould_get_bad_result_when_file_is_not_exist()
        {
            // arrange

            var sut = new FileReader();
            var fileName = "KeysNotExist.txt";
            var filePath = DirectoryHelper.GetPathToFileFromSourceFolder(fileName);

            // act

            var getKeysOperation = await sut.GetKeys(filePath);

            // assert

            Assert.False(getKeysOperation.Ok);
        }
    }
}