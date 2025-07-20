using CarAuctionMS.Auctions;
using CarAuctionMS.Bidding;
using CarAuctionMS.Entities;
using CarAuctionMS.Exceptions;

namespace CarAuctionMS.Managers
{
    public class AuctionManager: IAuctionManager
    {
        private readonly Dictionary<string, Vehicle> _vehicles = new();
        private readonly Dictionary<string, Auction> _auctions = new();

        public IEnumerable<Vehicle> GetAllVehicles()
        {
            return _vehicles.Values;
        }

        public IEnumerable<Auction> GetAllAuctions()
        {
            return _auctions.Values;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (_vehicles.ContainsKey(vehicle.Id))
                throw new DuplicateVehicleException(vehicle.Id);
            _vehicles[vehicle.Id] = vehicle;
        }

        public IEnumerable<Vehicle> Search(string? type = null,
                                          string? manufacturer = null,
                                          string? model = null,
                                          int? year = null)
        {
            

            

            return _vehicles.Values.Where(v =>
                (string.IsNullOrEmpty(type) || v.GetType().Name.Equals(type, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(manufacturer)  || v.Manufacturer.Equals(manufacturer, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(model) || v.Model.Equals(model, StringComparison.OrdinalIgnoreCase)) &&
                (!year.HasValue || v.Year == year.Value)
            );
        }

        public void StartAuction(string vehicleId)
        {
            if (!_vehicles.TryGetValue(vehicleId, out var vehicle))
                throw new VehicleNotFoundException(vehicleId);

            if (_auctions.TryGetValue(vehicleId, out var existing) && existing.IsActive)
                throw new AuctionAlreadyActiveException(vehicleId);

            var auction = new Auction(vehicle);
            auction.Start();
            _auctions[vehicleId] = auction;
        }

        public void PlaceBid(string vehicleId, string bidder, decimal amount)
        {
            if (!_auctions.TryGetValue(vehicleId, out var auction))
                throw new AuctionNotFoundException(vehicleId);

            auction.PlaceBid(new Bid(bidder, amount));
        }

        public void CloseAuction(string vehicleId)
        {
            if (!_auctions.TryGetValue(vehicleId, out var auction))
                throw new AuctionNotActiveException(vehicleId);

            auction.Close();
        }
    }
}
