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

    // TO-DO change: double -> instance of LoadProduct
    public override void LoadCargo(double massToLoad)
    {
        base.LoadCargo(massToLoad);
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