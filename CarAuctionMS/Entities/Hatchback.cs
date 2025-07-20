namespace CarAuctionMS.Entities
{
    public class Hatchback : Vehicle
    {
        public int NumberOfDoors { get; }
        public Hatchback(string id, string manufacturer, string model, int year, decimal startingBid, int doors)
            : base(id, manufacturer, model, year, startingBid)
        {
            NumberOfDoors = doors;
        }
    }
}
