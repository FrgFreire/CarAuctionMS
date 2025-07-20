using CarAuctionMS.Bidding;
using CarAuctionMS.Entities;
using CarAuctionMS.Exceptions;

namespace CarAuctionMS.Auctions
{
    public class Auction : IAuction
    {
        public Vehicle Vehicle { get; }
        public bool IsActive { get; private set; }
        public List<Bid> Bids { get; } = new();
        public Bid? HighestBid => Bids.OrderByDescending(b => b.Amount).FirstOrDefault();

        public Auction(Vehicle vehicle)
        {
            Vehicle = vehicle ?? throw new ArgumentNullException(nameof(vehicle));
        }

        public void Start()
        {
            if (IsActive)
                throw new AuctionAlreadyActiveException(Vehicle.Id);

            IsActive = true;
        }

        public void PlaceBid(Bid bid)
        {

            if (!IsActive) 
                throw new AuctionNotActiveException(Vehicle.Id);

            decimal current = HighestBid?.Amount ?? Vehicle.StartingBid;

            if (bid.Amount <= current)
                throw new InvalidBidException(Vehicle.Id, bid.Amount, current);

            Bids.Add(bid);
        }

        public void Close()
        {
            if (!IsActive) 
                throw new AuctionNotActiveException(Vehicle.Id);
            
            IsActive = false;
        }
    }
}
