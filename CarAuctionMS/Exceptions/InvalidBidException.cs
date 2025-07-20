namespace CarAuctionMS.Exceptions
{
    public class InvalidBidException : Exception
    {
        public InvalidBidException(string id, decimal bid, decimal current)
            : base($"Bid of {bid:C} for '{id}' must exceed current {current:C}.") { }
    }
}
