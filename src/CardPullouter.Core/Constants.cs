using CardPullouter.Core.Models;

namespace CardPullouter.Core
{
    public static class Constants
    {

        public const string KeysFileName = "Keys.txt";

        public const string ResultExcelFileName = "Cards.xslx";


        public static HtmlElement CardParentElement = new()
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

        public static HtmlElement LinkChildElement = new()
        {
            Attributes = new Dictionary<string, string>
            {
                { "class", "j-card-link"}
            }
        };

        public static HtmlElement TitleParentElement = new()
        {
            Attributes = new Dictionary<string, string>
            {
                { "class", "product-page__header-wrap"}
            },
        };

        public static HtmlElement TitleChildElement = new()
        {
            Attributes = new Dictionary<string, string>
            {
                { "data-link", "text{:selectedNomenclature^goodsName"}
            },
        };

        public static HtmlElement BrandParentElement = new()
        {
            Attributes = new Dictionary<string, string>
            {
                { "class", "product-page__seller-wrap"}
            },
        };

        public static HtmlElement BrandChildElement = new()
        {
            Attributes = new Dictionary<string, string>
            {
                { "class", "seller-info__name" }
            },
        };

        public static HtmlElement IdParentElement = new()
        {
            Attributes = new Dictionary<string, string>
            {
                { "class", "product-article"}
            },
        };

        public static HtmlElement IdChildElement = new()
        {
            Attributes = new Dictionary<string, string>
            {
                { "id", "productNmId" }
            },
        };

        public static HtmlElement FeedbacksParentElement = new()
        {
            Attributes = new Dictionary<string, string>
            {
                { "id", "comments_reviews_link"}
            },
        };

        public static HtmlElement FeedbacksChildElement = new()
        {
            Attributes = new Dictionary<string, string>
            {
                { "class", "product-review__count-review"}
            },
        };

        public static HtmlElement PriceParentElement = new()
        {
            Attributes = new Dictionary<string, string>
            {
                { "class", "j-price-block"}
            },
        };

        public static HtmlElement PriceChildElement = new()
        {
            Attributes = new Dictionary<string, string>
            {
                { "class", "price-block__old-price" }
            },
        };

    }
}
