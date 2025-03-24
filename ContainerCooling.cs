namespace Projekt_ZAJ3;

public class ContainerCooling : Container, IHazardNotifier
{
    public ContainerCooling(double temperature, double containerMass, int height, int depth, double maximumCapacity) : base(containerMass, height, depth, maximumCapacity)
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

    public override void LoadCargo(LoadProduct? cargoToLoad)
    {
        if (cargoToLoad == null)
            return;

        if (cargo != null && cargo.AllowedTemperature < cargoToLoad.AllowedTemperature)
        {
            Console.WriteLine("Container does not meet temperature requirement to transport this type of cargo.");
            return;
        }
        
        base.LoadCargo(cargoToLoad);
    }

    public double GetTemperature()
    {
        return temperature;
    }

    public void Notify()
    {
        {
            Console.WriteLine($"Critical Situation with Gas Cargo [{serialNumber}]");
        }
    }

    // Vars
    private double temperature;
}