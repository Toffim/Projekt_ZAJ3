namespace Projekt_ZAJ3;

public class ContainerGas : Container, IHazardNotifier
{
    public ContainerGas(double airPressure, double cargoMass, double containerMass, int height, int depth, double maximumCapacity) : base(cargoMass, containerMass, height, depth, maximumCapacity)
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
        cargoMass = (0.05 * cargoMass);
    }

    public override void LoadCargo(LoadProduct cargo)
    {
        base.LoadCargo(cargo);
    }

    public double GetAirPressure()
    {
        return airPressure;
    }

    public void Notify(string serialNumber)
    {
        {
            Console.WriteLine($"Critical Situation with Gas Cargo [{serialNumber}]");
        }
    }

    // Vars
    private double airPressure;
}