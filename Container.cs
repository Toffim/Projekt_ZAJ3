namespace Projekt_ZAJ3;

public class Container
{
    public Container(double cargoMass, double containerMass, int height, int depth, double maximumCapacity)
    {
        this.cargoMass = cargoMass;
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
        SetCargoMass(0);
    }

    public virtual void LoadCargo(LoadProduct cargo)
    {
        var massToLoad = cargo.TotalMass;

        if ((cargoMass + massToLoad) > maximumCapacity)
        {
            throw new OverfillException("Maximum capacity of cargo exceeded.");
        }
        SetCargoMass(cargoMass + massToLoad);
    }

    public void SetCargoMass(double cargoMass)
    {
        cargoMass = cargoMass;
    }

    public double GetCargoMass()
    {
        return cargoMass;
    }

    public string GetSerialNumber()
    {
        return serialNumber;
    }
// Vars
    public static int lastCargoIndex = 1;
    protected string serialNumber;

    protected double cargoMass;
    protected double containerMass;
    protected int height;
    protected int depth;
    protected double maximumCapacity;
}