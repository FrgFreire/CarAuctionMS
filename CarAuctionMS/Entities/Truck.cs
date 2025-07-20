namespace CarAuctionMS.Entities
{
    public class Truck : Vehicle
    {
        public double LoadCapacity { get; }
        public Truck(string id, string manufacturer, string model, int year, decimal startingBid, double capacity)
            : base(id, manufacturer, model, year, startingBid)
        {
            LoadCapacity = capacity;
        }
    }
}
