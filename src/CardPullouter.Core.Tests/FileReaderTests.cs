namespace CardPullouter.Core.Tests
{
    public class FileReaderTests
    {
        [Fact]
        public async Task ItShould_get_keys_from_file()
        {
            // arrange

            var sut = new FileReader();
            var binDirectory = Directory.GetCurrentDirectory();
            var currentDirectory = Directory.GetParent(binDirectory)!.Parent!.Parent!.FullName;
            var testFileName = "Keys.txt";
            var path = Path.Combine(currentDirectory, testFileName);

            // act

            var keys = await sut.GetKeys(path);

            // assert

            Assert.Contains("Игрушки", keys);
            Assert.Contains("Настолки", keys);
            Assert.Contains("Телефоны", keys);
        }
    }
}