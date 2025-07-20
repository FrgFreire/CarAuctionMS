namespace CarAuctionMS.Exceptions
{
    public class AuctionNotActiveException : Exception
    {
        public AuctionNotActiveException(string id)
            : base($"There is no active Auction for the vehicle '{id}'.") { }
    }
}
