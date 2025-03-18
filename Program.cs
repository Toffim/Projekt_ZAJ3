using Projekt_ZAJ3;

Console.WriteLine("Hello, World!");

Container container1 = new Container(1000, 500, 10, 5, 2000);
Console.WriteLine($"1 Container Serial Number: {container1.GetSerialNumber()}");

ContainerFluid fluidContainer = new ContainerFluid(false, 1000, 500, 10, 5, 2000);
Console.WriteLine($"2 Fluid Container Serial Number: {fluidContainer.GetSerialNumber()}");