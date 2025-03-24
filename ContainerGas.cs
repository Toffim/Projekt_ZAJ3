namespace Projekt_ZAJ3;

public class ContainerGas : Container, IHazardNotifier
{
    public ContainerGas(double airPressure, double containerMass, int height, int depth, double maximumCapacity) : base(containerMass, height, depth, maximumCapacity)
    {
        airPressure = airPressure;
    }

    protected override void GenerateSerialNumber()
    {
        serialNumber = "KON-" + "G-" + lastCargoIndex.ToString();
        Container.lastCargoIndex++;
    }

    public override void UnloadCargo()
    {
        if (cargo == null)
            return;
        cargo.TotalMass = (0.05 * cargo.TotalMass);
    }

    public override void LoadCargo(LoadProduct? cargoToLoad)
    {
        base.LoadCargo(cargoToLoad);
    }

    public double GetAirPressure()
    {
        return airPressure;
    }

    public void Notify()
    {
        Console.WriteLine($"Critical Situation with Gas Cargo [{serialNumber}]");
    }

    // Vars
    private double airPressure;
}