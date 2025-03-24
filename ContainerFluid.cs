namespace Projekt_ZAJ3;

public class ContainerFluid : Container, IHazardNotifier
{
    public ContainerFluid(double containerMass, int height, int depth, double maximumCapacity) :
        base(containerMass, height, depth, maximumCapacity)
    {}

    protected override void GenerateSerialNumber()
    {
        serialNumber = "KON-" + "F-" + lastCargoIndex.ToString();
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

        // Cargo is null -> initialize it.
        cargo ??= cargoToLoad;
        var massToLoad = cargoToLoad.TotalMass;

        var reasonableCap = cargoToLoad.UnsafeCargo ? maximumCapacity * 0.5 : maximumCapacity * 0.9;
        if (cargo.TotalMass + massToLoad > reasonableCap)
        {
            Notify();
        }

        base.LoadCargo(cargo);
    }

    public void Notify()
    {
        Console.WriteLine($"Critical Situation with Fluid Cargo [{serialNumber}]");
    }
}