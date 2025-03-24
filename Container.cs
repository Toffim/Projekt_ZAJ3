namespace Projekt_ZAJ3;

public class Container
{
    public Container(double containerMass, int height, int depth, double maximumCapacity)
    {
        this.containerMass = containerMass;
        this.height = height;
        this.depth = depth;
        this.maximumCapacity = maximumCapacity;

        GenerateSerialNumber();
    }

    protected virtual void GenerateSerialNumber()
    {
        serialNumber = "KON-" + "BASE-" + lastCargoIndex.ToString();
        lastCargoIndex++;
    }

    public virtual void UnloadCargo()
    {
        cargo = null;
    }

    public virtual void LoadCargo(LoadProduct? cargoToLoad)
    {
        if (cargoToLoad == null)
            return;

        if (cargo != null && cargo.TypeName != cargoToLoad.TypeName)
        {
            Console.WriteLine("Canot load cargo of different types into the container.");
            return;
        }

        // Cargo is null -> initialize it.
        cargo ??= cargoToLoad;

        var massToLoad = cargoToLoad.TotalMass;

        if ((cargo.TotalMass + massToLoad) > maximumCapacity)
        {
            throw new OverfillException("Maximum capacity of cargo exceeded.");
        }

        SetCargoMass(cargo.TotalMass + massToLoad);
    }

    public void SetCargoMass(double cargoMass)
    {
        cargo.TotalMass = cargoMass;
    }

    public double GetCargoMass()
    {
        return cargo.TotalMass;
    }

    public string GetSerialNumber()
    {
        return serialNumber;
    }
    
    public override string ToString()
    {
        return ($"Kontener {GetSerialNumber()} (maxCap={maximumCapacity}, containerMass={containerMass}, height={height}, depth={depth})");
    }
// Vars
    public static int lastCargoIndex = 1;
    protected string serialNumber;

    protected LoadProduct? cargo;
    protected double containerMass;
    protected int height;
    protected int depth;
    protected double maximumCapacity;
}