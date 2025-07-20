using CarAuctionMS.Auctions;
using CarAuctionMS.Entities;

namespace CarAuctionMS.Managers
{
    public interface IAuctionManager
    {
        IEnumerable<Vehicle> GetAllVehicles();
        IEnumerable<Auction> GetAllAuctions();
        void AddVehicle(Vehicle vehicle);
        IEnumerable<Vehicle> Search(string? type = null, string? manufacturer = null, string? model = null, int? year = null);
        void StartAuction(string vehicleId);
        void PlaceBid(string vehicleId, string bidder, decimal amount);
        void CloseAuction(string vehicleId);
    }
}
