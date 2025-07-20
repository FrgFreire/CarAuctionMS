namespace CarAuctionMS.Exceptions
{
    public class AuctionNotFoundException : Exception
    {
        public AuctionNotFoundException(string id)
            : base($"Auction not found for the vehicle '{id}'.") { }
    }
}
