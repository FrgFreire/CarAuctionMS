namespace CarAuctionMS.Exceptions
{
    public class DuplicateVehicleException : Exception
    {
        public DuplicateVehicleException(string id)
            : base($"Vehicle with ID '{id}' already exists.") { }
    }
}
