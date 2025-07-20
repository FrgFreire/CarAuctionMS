namespace CarAuctionMS.Entities
{
    public class SUV : Vehicle
    {
        public int NumberOfSeats { get; }
        public SUV(string id, string manufacturer, string model, int year, decimal startingBid, int seats)
            : base(id, manufacturer, model, year, startingBid)
        {
            NumberOfSeats = seats;
        }
    }
}
