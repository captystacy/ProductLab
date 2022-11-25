namespace CardPullouter.Core.Models
{
    public class Card
    {
        public string Title { get; }
        public string Brand { get; }
        public long Id { get; }
        public int FeedBacks { get; }
        public decimal Price { get; }

        public Card(string title, string brand, long id, int feedBacks, decimal price)
        {
            Title = title;
            Brand = brand;
            Id = id;
            FeedBacks = feedBacks;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Title}, {nameof(Brand)}: {Brand}, {nameof(Id)}: {Id}, {nameof(FeedBacks)}: {FeedBacks}, {nameof(Price)}: {Price}";
        }
    }
}
