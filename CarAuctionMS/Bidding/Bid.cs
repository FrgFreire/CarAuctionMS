namespace CarAuctionMS.Bidding
{
    public class Bid
    {
        public string Bidder { get; }
        public decimal Amount { get; }
        public DateTime Time { get; }

        public Bid(string bidder, decimal amount)
        {
            Bidder = !string.IsNullOrEmpty(bidder) ? bidder : throw new ArgumentNullException(nameof(bidder));
            Amount = amount >= 0 ? amount : throw new ArgumentOutOfRangeException(nameof(amount));
            Time = DateTime.UtcNow;
        }
    }
}
