using CarAuctionMS.Auctions;
using CarAuctionMS.Entities;
using CarAuctionMS.Exceptions;
using CarAuctionMS.Managers;

namespace CarAuctionMS.TestProject
{
    public class AuctionManagerTests
    {
        private readonly AuctionManager manager = new();

        [Fact]
        public void AddVehicle_UniqueId_Succeeds()
        {
            var car = new Sedan("S1", "Toyota", "Camry", 2020, 10000m, 4);
            manager.AddVehicle(car);
            var results = manager.Search(type: "Sedan");
            Assert.Contains(car, results);
        }

        [Fact]
        public void AddVehicle_DuplicateId_Throws()
        {
            var carA = new Hatchback("H1", "Honda", "Fit", 2021, 8000m, 4);
            var carB = new Hatchback("H1", "Ford", "Focus", 2019, 7000m, 4);
            manager.AddVehicle(carA);
            Assert.Throws<DuplicateVehicleException>(() => manager.AddVehicle(carB));
        }

        [Fact]
        public void StartAuction_NonexistentVehicle_Throws()
        {
            Assert.Throws<VehicleNotFoundException>(() => manager.StartAuction("X1"));
        }

        [Fact]
        public void StartAndPlaceBid_Succeeds()
        {
            var suv = new SUV("U1", "Jeep", "Wrangler", 2022, 15000m, 5);
            manager.AddVehicle(suv);
            manager.StartAuction("U1");
            manager.PlaceBid("U1", "Alice", 15500m);
            manager.PlaceBid("U1", "Bob", 16000m);
            // Accessing via reflection to assert highest bid:
            var auctionField = typeof(AuctionManager)
                .GetField("_auctions", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            var auctions = (System.Collections.IDictionary)auctionField.GetValue(manager);
            var auction = (Auction)auctions["U1"];
            Assert.Equal(16000m, auction.HighestBid.Amount);
            Assert.Equal("Bob", auction.HighestBid.Bidder);
        }

        [Fact]
        public void PlaceBid_LowerThanCurrent_Throws()
        {
            var truck = new Truck("T1", "Volvo", "FH", 2021, 20000m, 18_000);
            manager.AddVehicle(truck);
            manager.StartAuction("T1");
            manager.PlaceBid("T1", "Alice", 21000m);
            Assert.Throws<InvalidBidException>(() => manager.PlaceBid("T1", "Bob", 20500m));
        }

        [Fact]
        public void CloseAuction_NotActive_Throws()
        {
            var car = new Sedan("S2", "BMW", "3 Series", 2023, 25000m, 4);
            manager.AddVehicle(car);
            Assert.Throws<AuctionNotFoundException>(() => manager.CloseAuction("S2"));
        }
    }
}