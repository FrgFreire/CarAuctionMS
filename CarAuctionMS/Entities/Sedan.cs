namespace CarAuctionMS.Entities
{
    public class Sedan : Vehicle
    {
        public int NumberOfDoors { get; }
        public Sedan(string id, string manufacturer, string model, int year, decimal startingBid, int doors)
            : base(id, manufacturer, model, year, startingBid)
        {
            NumberOfDoors = doors;
        }
    }
}
