namespace CarAuctionMS.Exceptions
{
    public class VehicleNotFoundException : Exception
    {
        public VehicleNotFoundException(string id)
            : base($"Vehicle with ID '{id}' not found.") { }
    }
}
