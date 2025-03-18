namespace Projekt_ZAJ3;

public class Container
{
    private Container(int cargoMass, int massContainer, int height, int depth, int maximumCapacity)
    {
        this.cargoMass = cargoMass;
        this.massContainer = massContainer;
        this.height = height;
        this.depth = depth;
        this.maximumCapacity = maximumCapacity;

        GenerateSerialNumber();
    }

    protected virtual void GenerateSerialNumber()
    {
        serialNumber = "KON-" + "B-" + lastCargoIndex.ToString();
        lastCargoIndex++;
    }

    public void UnloadCargo()
    {
        SetCargoMass(0);
    }

    public void LoadCargo(int cargoMass)
    {
        cargoMass = cargoMass;
    }

    public void SetCargoMass(int cargoMass)
    {
        if (cargoMass > maximumCapacity)
        {
            throw new OverfillException("Maximum capacity of cargo exceeded.");
        }
        cargoMass = cargoMass;
    }

    public int GetCargoMass()
    {
        return cargoMass;
    }
// Vars
    private static int lastCargoIndex = 1;
    private string serialNumber;

    public int cargoMass;
    private int massContainer;
    private int height;
    private int depth;
    private int maximumCapacity;
}