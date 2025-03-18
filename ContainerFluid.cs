namespace Projekt_ZAJ3;

public class ContainerFluid : Container, IHazardNotifier
{
    public ContainerFluid(bool unsafeCargo, double cargoMass, double containerMass, int height, int depth, double maximumCapacity) : base(cargoMass, containerMass, height, depth, maximumCapacity)
    {
        unsafeCargo = unsafeCargo;
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

    public override void LoadCargo(double cargoMass)
    {
        var reasonableCap = IsWithUnsafeCargo() ? maximumCapacity * 0.5 : maximumCapacity * 0.9;
        if (cargoMass > reasonableCap)
        {
            Notify(GetSerialNumber());
        }

        base.UnloadCargo();
    }

    public bool IsWithUnsafeCargo()
    {
        return unsafeCargo;
    }

    public void Notify(string serialNumber)
    {
        {
            Console.WriteLine($"Critical Situation with Fluid Cargo [{serialNumber}]");
        }
    }

    // Vars
    private bool unsafeCargo = false;
}