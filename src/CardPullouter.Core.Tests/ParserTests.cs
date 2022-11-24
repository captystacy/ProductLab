using CardPullouter.Core.Tests.Helpers;

namespace CardPullouter.Core.Tests
{
    public class ParserTests
    {
        [Fact]
        public async Task ItShould_get_hrefs()
        {
            // arrange

            var fileName = "page.html";
            var filePath = DirectoryHelper.GetPathToFileFromSourceFolder(fileName);
            var html = await File.ReadAllTextAsync(filePath);

            var sut = new Parser();

            var typicalParentElement = new HtmlElement("div")
            {
                Attributes = new Dictionary<string, string>
                {
                    { "class", "j-card-item"}
                },
                AvoidableAttributes = new Dictionary<string, string>
                {
                    { "class", "j-advert-card-item"}
                }
            };

            var targetChildElement = new HtmlElement("a")
            {
                Attributes = new Dictionary<string, string>
                {
                    { "class", "j-card-link"}
                }
            };

            // act

            var getHrefsOperation = await sut.GetHrefs(html, typicalParentElement, targetChildElement);

            // assert

            Assert.True(getHrefsOperation.Ok);
            Assert.NotNull(getHrefsOperation.Result);
            Assert.Equal(100, getHrefsOperation.Result.Count());
            foreach (var href in getHrefsOperation.Result)
            {
                Assert.NotNull(href);
                Assert.NotEmpty(href);
            }
        }
    }
}
