using CarAuctionMS.Bidding;
using CarAuctionMS.Entities;

namespace CarAuctionMS.Auctions
{
    public interface IAuction
    {
        Vehicle Vehicle { get; }
        bool IsActive { get; }
        List<Bid> Bids { get; }
        Bid? HighestBid { get; }
        void Start();
        void PlaceBid(Bid bid);
        void Close();
    }
}
