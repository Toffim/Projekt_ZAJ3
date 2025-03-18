namespace Projekt_ZAJ3;

public class ContainerCooling : Container, IHazardNotifier
{
    public ContainerCooling(double temperature, double cargoMass, double containerMass, int height, int depth, double maximumCapacity) : base(cargoMass, containerMass, height, depth, maximumCapacity)
    {
        temperature = temperature;
    }

    protected override void GenerateSerialNumber()
    {
        serialNumber = "KON-" + "C-" + lastCargoIndex.ToString();
        Container.lastCargoIndex++;
    }

    public override void UnloadCargo()
    {
        base.UnloadCargo();
    }

    public override void LoadCargo(LoadProduct cargo)
    {
        base.LoadCargo(cargo);
    }

    public double GetTemperature()
    {
        return temperature;
    }

    public void Notify(string serialNumber)
    {
        {
            Console.WriteLine($"Critical Situation with Gas Cargo [{serialNumber}]");
        }
    }

    // Vars
    private double temperature;
}