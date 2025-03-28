﻿namespace Projekt_ZAJ3;

public class LoadProduct
{
    public string TypeName
    {
        get => typeName;
        set => typeName = value ?? throw new ArgumentNullException(nameof(value));
    }

    public double TotalMass
    {
        get => totalMass;
        set => totalMass = value;
    }

    public double AllowedTemperature
    {
        get => allowedTemperature;
        set => allowedTemperature = value;
    }
    
    public bool UnsafeCargo
    {
        get => unsafeCargo;
        set => unsafeCargo = value;
    }

    public LoadProduct(string typeName, double totalMass, double allowedTemperature, bool unsafeCargo)
    {
        this.typeName = typeName;
        this.totalMass = totalMass;
        this.allowedTemperature = allowedTemperature;
        this.unsafeCargo = unsafeCargo;
    }

    private string typeName;
    private double totalMass;
    private bool unsafeCargo;
    private double allowedTemperature;
}