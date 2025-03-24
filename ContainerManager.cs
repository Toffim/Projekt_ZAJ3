namespace Projekt_ZAJ3;

public class ContainerManager
{
    public List<Container> listContainers = new List<Container>();
    public List<ContainerShip> listShips = new List<ContainerShip>();

    public enum ChoiceOptions
    {
        MENU_MAIN = 0,
        MENU_CONTAINERSHIP = 1,
        MENU_CONTAINER = 2
    }

    public void run()
    {
        ChoiceOptions chosenOption = ChoiceOptions.MENU_MAIN;
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Lista kontenerowców:");
            if (listShips.Count == 0) {Console.WriteLine("Brak");}
            else foreach (var ship in listShips) Console.WriteLine($"- {ship}");

            Console.WriteLine("\nLista kontenerów:");
            if (listContainers.Count == 0) Console.WriteLine("Brak");
            else foreach (var container in listContainers) Console.WriteLine($"- {container}");

            Console.WriteLine("\nMożliwe akcje:");
            switch (chosenOption)
            {
                case ChoiceOptions.MENU_MAIN:
                    ShowMainMenu();
                    break;

                case ChoiceOptions.MENU_CONTAINERSHIP:
                    ShowContainerShipMenu();
                    break;

                case ChoiceOptions.MENU_CONTAINER:
                    ShowContainerMenu();
                    break;
            }
            Console.WriteLine("0. Wyjdź");

            Console.Write("Wybierz opcję: ");
            int choice = int.Parse(Console.ReadLine() ?? "0");

            switch (choice)
            {
                case 1:
                    if (chosenOption == ChoiceOptions.MENU_MAIN) AddContainerShip();
                    else if (chosenOption == ChoiceOptions.MENU_CONTAINERSHIP) AddContainer();
                    else if (chosenOption == ChoiceOptions.MENU_CONTAINER) LoadCargo();
                    break;

                case 2:
                    if (chosenOption == ChoiceOptions.MENU_MAIN) RemoveContainerShip();
                    else if (chosenOption == ChoiceOptions.MENU_CONTAINERSHIP) RemoveContainer();
                    else if (chosenOption == ChoiceOptions.MENU_CONTAINER) UnloadContainer();
                    break;

                case 3:
                    if (chosenOption == ChoiceOptions.MENU_MAIN) SelectContainerShip(ref chosenOption);
                    else if (chosenOption == ChoiceOptions.MENU_CONTAINERSHIP) SelectContainer(ref chosenOption);
                    break;

                case 4:
                    if (chosenOption == ChoiceOptions.MENU_CONTAINERSHIP) GoBackToMainMenu(ref chosenOption);
                    else if (chosenOption == ChoiceOptions.MENU_CONTAINER) GoBackToContainerShipMenu(ref chosenOption);
                    break;

                case 0:
                    return;

                default:
                    Console.WriteLine("Nieprawidłowa opcja. Spróbuj ponownie.");
                    break;
            }
            Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
            Console.ReadKey();
        }
    }
    
    private void ShowMainMenu()
    {
        Console.WriteLine("1. Dodaj kontenerowiec");
        Console.WriteLine("2. Usun kontenerowiec");
        Console.WriteLine("3. Wybierz kontenerowiec");
    }

    private void ShowContainerShipMenu()
    {
        Console.WriteLine("1. Dodaj kontener");
        Console.WriteLine("2. Usun kontener");
        Console.WriteLine("3. Wybierz kontener");
        Console.WriteLine("4. Wroc");
    }

    private void ShowContainerMenu()
    {
        Console.WriteLine("1. Zaladuj kontener (tworzy ladunek)");
        Console.WriteLine("2. Wyladuj kontener");
        Console.WriteLine("4. Wroc");
    }

    private void AddContainerShip()
    {
        Console.Write("Kontenerowiec dodawany...");
        Console.Write("Podaj maksymalną prędkość (w węzłach): ");
        double speed = double.Parse(Console.ReadLine() ?? "0");

        Console.Write("Podaj maksymalną liczbę kontenerów: ");
        int maxContainers = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Podaj maksymalną wagę ładunku (w tonach): ");
        double maxWeight = double.Parse(Console.ReadLine() ?? "0");

        listShips.Add(new ContainerShip(speed, maxContainers, maxWeight));
        Console.WriteLine("Kontenerowiec dodany.");
    }

    private void AddContainer()
    {
        Console.WriteLine("Wybierz typ kontenera:");
        Console.WriteLine("1. Kontener ogólnego przeznaczenia");
        Console.WriteLine("2. Kontener na płyny");
        Console.WriteLine("3. Kontener na gaz");
        Console.WriteLine("4. Kontener chłodniczy");
        Console.Write("Wybierz opcję: ");
        int type = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Podaj masę własną kontenera (kg): ");
        double mass = double.Parse(Console.ReadLine() ?? "0");

        Console.Write("Podaj wysokość kontenera (cm): ");
        int height = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Podaj głębokość kontenera (cm): ");
        int depth = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("Podaj maksymalną pojemność kontenera (kg): ");
        double maxCapacity = double.Parse(Console.ReadLine() ?? "0");

        Container container = null;
        switch (type)
        {
            case 1:
                container = new Container(mass, height, depth, maxCapacity);
                break;
            case 2:
                container = new ContainerFluid(mass, height, depth, maxCapacity);
                break;
            case 3:
                Console.Write("Podaj ciśnienie gazu (atm): ");
                double pressure = double.Parse(Console.ReadLine() ?? "0");
                container = new ContainerGas(pressure, mass, height, depth, maxCapacity);
                break;
            case 4:
                Console.Write("Podaj temperaturę kontenera (°C): ");
                double temp = double.Parse(Console.ReadLine() ?? "0");
                container = new ContainerCooling(temp, mass, height, depth, maxCapacity);
                break;
            default:
                Console.WriteLine("Nieprawidłowy typ kontenera.");
                return;
        }

        listContainers.Add(container);
        Console.WriteLine($"Kontener dodany. Numer seryjny: {container.GetSerialNumber()}");
    }

    private void LoadCargo()
    {
        if (listContainers.Count == 0)
        {
            Console.WriteLine("Brak dostępnych kontenerów.");
            return;
        }

        Console.Write("Podaj numer seryjny kontenera: ");
        string serial = Console.ReadLine() ?? "";

        var container = listContainers.FirstOrDefault(c => c.GetSerialNumber() == serial);
        if (container == null)
        {
            Console.WriteLine("Nie znaleziono kontenera.");
            return;
        }

        if (container is ContainerCooling)
        {
            // For cooling containers, show available food products
            Console.WriteLine("Dostępne produkty żywnościowe:");
            var foodProducts = new List<(string Name, double Temperature, bool IsUnsafe)>
            {
                ("Bananas", 13.3, false),
                ("Chocolate", 18, false),
                ("Fish", 2, true),
                ("Meat", -15, true),
                ("Ice cream", -18, false),
                ("Frozen pizza", -30, false),
                ("Cheese", 7.2, false),
                ("Sausages", 5, true),
                ("Butter", 20.5, false),
                ("Eggs", 19, false)
            };

            for (int i = 0; i < foodProducts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {foodProducts[i].Name} (temp: {foodProducts[i].Temperature}°C)");
            }

            Console.Write("Wybierz produkt: ");
            int productChoice = int.Parse(Console.ReadLine() ?? "0") - 1;

            if (productChoice < 0 || productChoice >= foodProducts.Count)
            {
                Console.WriteLine("Nieprawidłowy wybór produktu.");
                return;
            }

            Console.Write("Podaj masę ładunku (kg): ");
            double weight = double.Parse(Console.ReadLine() ?? "0");

            var selectedProduct = foodProducts[productChoice];
            var cargo = new LoadProduct(selectedProduct.Name, weight, selectedProduct.Temperature, selectedProduct.IsUnsafe);
            
            try
            {
                container.LoadCargo(cargo);
                Console.WriteLine("Ładunek załadowany.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Błąd: {e.Message}");
            }
        }
        else
        {
            // For other container types
            Console.Write("Podaj nazwę ładunku: ");
            string name = Console.ReadLine() ?? "";

            Console.Write("Podaj masę ładunku (kg): ");
            double weight = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Czy ładunek jest niebezpieczny? (t/n): ");
            bool isHazardous = Console.ReadLine()?.ToLower() == "t";

            var cargo = new LoadProduct(name, weight, 0, isHazardous);
            
            try
            {
                container.LoadCargo(cargo);
                Console.WriteLine("Ładunek załadowany.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Błąd: {e.Message}");
            }
        }
    }

    private void RemoveContainerShip()
    {
        if (listShips.Count == 0) { Console.WriteLine("Brak dostępnych kontenerowcow."); return; }

        Console.Write("Podaj numer kontenerowca do usunięcia: ");
        int shipNumber = int.Parse(Console.ReadLine() ?? "0");
        var ship = listShips.FirstOrDefault(s => s.ShipNumber == shipNumber);
        if (ship == null) { Console.WriteLine("Nie znaleziono kontenerowca."); return; }

        listShips.Remove(ship);
        Console.WriteLine("Kontenerowiec usunięty.");
    }

    private void RemoveContainer()
    {
        if (listContainers.Count == 0) { Console.WriteLine("Brak dostępnych kontenerów."); return; }

        Console.Write("Podaj numer seryjny kontenera do usunięcia: ");
        string serial = Console.ReadLine() ?? "";
        var container = listContainers.FirstOrDefault(c => c.GetSerialNumber() == serial);
        if (container == null) { Console.WriteLine("Nie znaleziono kontenera."); return; }

        listContainers.Remove(container);
        Console.WriteLine("Kontener usunięty.");
    }

    private void SelectContainerShip(ref ChoiceOptions chosenOption)
    {
        Console.Write("Podaj numer kontenerowca, aby przejść do zarządzania kontenerami: ");
        int shipNumber = int.Parse(Console.ReadLine() ?? "0");
        var ship = listShips.FirstOrDefault(s => s.ShipNumber == shipNumber);
        if (ship == null) { Console.WriteLine("Nie znaleziono kontenerowca."); return; }

        chosenOption = ChoiceOptions.MENU_CONTAINERSHIP;
        Console.WriteLine($"Przełączono do zarządzania kontenerowcem: {shipNumber}");
    }

    private void SelectContainer(ref ChoiceOptions chosenOption)
    {
        Console.Write("Podaj numer seryjny kontenera, aby przejść do jego menu: ");
        string serial = Console.ReadLine() ?? "";
        var container = listContainers.FirstOrDefault(c => c.GetSerialNumber() == serial);
        if (container == null) { Console.WriteLine("Nie znaleziono kontenera."); return; }

        chosenOption = ChoiceOptions.MENU_CONTAINER;
        Console.WriteLine($"Przełączono do zarządzania kontenerem: {serial}");
    }

    private void GoBackToMainMenu(ref ChoiceOptions chosenOption)
    {
        chosenOption = ChoiceOptions.MENU_MAIN;
        Console.WriteLine("Wrócono do głównego menu.");
    }

    private void GoBackToContainerShipMenu(ref ChoiceOptions chosenOption)
    {
        chosenOption = ChoiceOptions.MENU_CONTAINERSHIP;
        Console.WriteLine("Wrócono do menu kontenerowców.");
    }

    private void UnloadContainer()
    {
        Console.Write("Podaj numer seryjny kontenera do wyładunku: ");
        string serial = Console.ReadLine() ?? "";
        var container = listContainers.FirstOrDefault(c => c.GetSerialNumber() == serial);
        if (container == null) { Console.WriteLine("Nie znaleziono kontenera."); return; }

        container.UnloadCargo();
        Console.WriteLine("Kontener został wyładowany.");
    }
}