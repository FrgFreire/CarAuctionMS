namespace CarAuctionMS.Exceptions
{
    public class AuctionAlreadyActiveException : Exception
    {
        public AuctionAlreadyActiveException(string id)
            : base($"There is an Auction for the vehicle '{id}' already active.") { }
    }
}
