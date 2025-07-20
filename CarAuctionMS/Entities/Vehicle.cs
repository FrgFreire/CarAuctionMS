namespace CarAuctionMS.Entities
{
    public abstract class Vehicle
    {
        public string Id { get; }
        public string Manufacturer { get; }
        public string Model { get; }
        public int Year { get; }
        public decimal StartingBid { get; }

        protected Vehicle(string id, string manufacturer, string model, int year, decimal startingBid)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Manufacturer = manufacturer ?? throw new ArgumentNullException(nameof(manufacturer));
            Model = model ?? throw new ArgumentNullException(nameof(model));
            Year = year;
            StartingBid = startingBid >= 0 ? startingBid : throw new ArgumentOutOfRangeException(nameof(startingBid));
        }
    }
}
