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

            var typicalParentElement = new HtmlElement()
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

            var targetChildElement = new HtmlElement()
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

        [Fact]
        public async Task ItShould_get_inner_text()
        {
            // arrange

            var fileName = "page.html";
            var filePath = DirectoryHelper.GetPathToFileFromSourceFolder(fileName);
            var html = await File.ReadAllTextAsync(filePath);

            var sut = new Parser();

            var typicalParentElement = new HtmlElement()
            {
                Attributes = new Dictionary<string, string>
                {
                    { "class", "searching-results__inner"}
                }
            };

            var targetChildElement = new HtmlElement()
            {
                Attributes = new Dictionary<string, string>
                {
                    { "class", "searching-results__title"}
                }
            };

            // act

            var getInnerTextOperation = await sut.GetInnerText(html, typicalParentElement, targetChildElement);

            // assert

            Assert.True(getInnerTextOperation.Ok);
            Assert.NotNull(getInnerTextOperation.Result);
            Assert.Equal("По запросу «мебель» найдено", getInnerTextOperation.Result);
        }
    }
}
