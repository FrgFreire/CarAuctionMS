using CarAuctionMS.Entities;
using CarAuctionMS.Managers;

class Program
{
    static void Main(string[] args)
    {
        IAuctionManager manager = new AuctionManager();
        bool exitRequested = false;

        while (!exitRequested)
        {
            Console.WriteLine();
            Console.WriteLine("=== Car Auction Management ===");
            Console.WriteLine("1. Add Vehicle");
            Console.WriteLine("2. Search Vehicles");
            Console.WriteLine("3. Start Auction");
            Console.WriteLine("4. Place Bid");
            Console.WriteLine("5. Close Auction");
            Console.WriteLine("6. List All Vehicles");
            Console.WriteLine("7. List All Auctions");
            Console.WriteLine("8. Exit");
            Console.Write("Select an option: ");
            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        AddVehicleFlow(manager);
                        break;
                    case "2":
                        SearchVehiclesFlow(manager);
                        break;
                    case "3":
                        StartAuctionFlow(manager);
                        break;
                    case "4":
                        PlaceBidFlow(manager);
                        break;
                    case "5":
                        CloseAuctionFlow(manager);
                        break;
                    case "6":
                        GetAllVehiclesFlow(manager);
                        break;
                    case "7":
                        GetAllAuctionsFlow(manager);
                        break;
                    case "8":
                        exitRequested = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        Console.WriteLine("Goodbye!");
    }

    private static void AddVehicleFlow(IAuctionManager manager)
    {
        Console.Write("Vehicle Type (Sedan, Hatchback, SUV, Truck): ");
        var type = Console.ReadLine()?.Trim();
        Console.Write("ID: ");
        var id = Console.ReadLine()?.Trim();
        Console.Write("Manufacturer: ");
        var manufacturer = Console.ReadLine()?.Trim();
        Console.Write("Model: ");
        var model = Console.ReadLine()?.Trim();
        Console.Write("Year: ");
        var year = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Starting Bid: ");
        var startingBid = decimal.Parse(Console.ReadLine() ?? "0");

        Vehicle vehicle = type?.ToLower() switch
        {
            "sedan" => new Sedan(id, manufacturer, model, year, startingBid, ReadInt("Number of Doors: ")),
            "hatchback" => new Hatchback(id, manufacturer, model, year, startingBid, ReadInt("Number of Doors: ")),
            "suv" => new SUV(id, manufacturer, model, year, startingBid, ReadInt("Number of Seats: ")),
            "truck" => new Truck(id, manufacturer, model, year, startingBid, ReadDouble("Load Capacity (tons): ")),
            _ => throw new InvalidOperationException("Unknown vehicle type")
        };

        manager.AddVehicle(vehicle);
        Console.WriteLine($"Added {type} with ID {id}.");
    }

    private static void SearchVehiclesFlow(IAuctionManager manager)
    {
        Console.Write("Type filter (or blank): ");
        var t = Console.ReadLine();
        Console.Write("Manufacturer filter (or blank): ");
        var m = Console.ReadLine();
        Console.Write("Model filter (or blank): ");
        var mo = Console.ReadLine();
        Console.Write("Year filter (or blank): ");
        var yInput = Console.ReadLine();
        int? y = int.TryParse(yInput, out var yVal) ? yVal : (int?)null;

        var results = manager.Search(t, m, mo, y);
        Console.WriteLine($"Found {results.Count()} vehicle(s):");
        foreach (var v in results)
        {
            Console.WriteLine($"- [{v.Id}] {v.GetType().Name} {v.Manufacturer} {v.Model} ({v.Year}), StartBid: {v.StartingBid:C}");
        }
    }

    private static void GetAllVehiclesFlow(IAuctionManager manager)
    {
        var results = manager.GetAllVehicles();
        Console.WriteLine($"Found {results.Count()} vehicle(s):");
        foreach (var v in results)
        {
            Console.WriteLine($"- [{v.Id}] {v.GetType().Name} {v.Manufacturer} {v.Model} ({v.Year}), StartBid: {v.StartingBid:C}");
        }
    }

    private static void GetAllAuctionsFlow(IAuctionManager manager)
    {
        var results = manager.GetAllAuctions();
        Console.WriteLine($"Found {results.Count()} vehicle(s):");
        foreach (var auction in results)
        {
            Console.WriteLine($"- Auction is Active: {auction.IsActive} [{auction.Vehicle.Id}] {auction.Vehicle.GetType().Name} {auction.Vehicle.Manufacturer} {auction.Vehicle.Model} ({auction.Vehicle.Year}), StartBid: {auction.Vehicle.StartingBid:C}, HighestBid:{auction.HighestBid?.Amount:C}");
        }
    }

    private static void StartAuctionFlow(IAuctionManager manager)
    {
        Console.Write("Vehicle ID to start auction: ");
        var vid = Console.ReadLine();
        manager.StartAuction(vid);
        Console.WriteLine($"Auction started for vehicle {vid}.");
    }

    private static void PlaceBidFlow(IAuctionManager manager)
    {
        Console.Write("Vehicle ID to bid on: ");
        var vid = Console.ReadLine();
        Console.Write("Your name: ");
        var bidder = Console.ReadLine();
        Console.Write("Bid amount: ");
        var amount = decimal.Parse(Console.ReadLine() ?? "0");
        manager.PlaceBid(vid, bidder, amount);
        Console.WriteLine($"Bid placed by {bidder} for {amount:C} on {vid}.");
    }

    private static void CloseAuctionFlow(IAuctionManager manager)
    {
        Console.Write("Vehicle ID to close auction: ");
        var vid = Console.ReadLine();
        manager.CloseAuction(vid);
        Console.WriteLine($"Auction closed for {vid}.");
    }

    private static int ReadInt(string prompt)
    {
        Console.Write(prompt);
        return int.Parse(Console.ReadLine() ?? "0");
    }

    private static double ReadDouble(string prompt)
    {
        Console.Write(prompt);
        return double.Parse(Console.ReadLine() ?? "0");
    }
}