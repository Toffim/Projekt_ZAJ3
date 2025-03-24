namespace Projekt_ZAJ3;

public class ContainerShip
{
    public static int GlobalNumber = 0;

    private int shipNumber;
    public double ShipNumber { get => shipNumber; }

    public List<Container> Containers { get; } = new();
    public double MaxSpeed { get; }
    public int MaxContainers { get; }
    public double MaxWeightTons { get; }

    public ContainerShip(double maxSpeed, int maxContainers, double maxWeightTons)
    {
        MaxSpeed = maxSpeed;
        MaxContainers = maxContainers;
        MaxWeightTons = maxWeightTons;

        shipNumber = GlobalNumber++;
    }

    public bool LoadContainer(Container container)
    {
        if (Containers.Count >= MaxContainers)
        {
            Console.WriteLine("Cannot load: Maximum container capacity reached.");
            return false;
        }

        double totalWeight = Containers.Sum(c => c.GetCargoMass()) + container.GetCargoMass();
        if (totalWeight > MaxWeightTons * 1000) // tons to kg
        {
            Console.WriteLine("Cannot load: Maximum weight capacity exceeded.");
            return false;
        }

        Containers.Add(container);
        return true;
    }

    public bool UnloadContainer(Container container)
    {
        if (!Containers.Contains(container))
        {
            Console.WriteLine("Container not found on the ship.");
            return false;
        }

        Containers.Remove(container);
        return true;
    }

    public double GetTotalCargoWeight()
    {
        return Containers.Sum(c => c.GetCargoMass()) / 1000; // kg into tons
    }

    public void DisplayShipInfo()
    {
        // $ allows to put vars without concatenation, quite cool
        Console.WriteLine($"Container Ship Info:");
        Console.WriteLine($"-> Max Speed: {MaxSpeed} in knots");
        Console.WriteLine($"-> Max Containers: {MaxContainers}");
        Console.WriteLine($"-> Max Weight: {MaxWeightTons} tons");
        Console.WriteLine($"-> Current Containers: {Containers.Count}");
        Console.WriteLine($"-> Current Weight: {GetTotalCargoWeight()} tons");
    }
}