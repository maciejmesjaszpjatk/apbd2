using Cwiczenia2;

Console.WriteLine("--- Container Loading/Unloading Tests ---");

try
{
    LiquidContainer milkContainer = new LiquidContainer(100, 200, 50, 150, 1000, false);
    milkContainer.LoadContainer(700);
    milkContainer.LoadContainer(150); // error expcted
}
catch (Exception e)
{
    Console.WriteLine($"Caught expected exception: {e.Message}");
}

try
{
    GasContainer gasContainer = new GasContainer(200, 250, 100, 200, 800, false, 10);
    gasContainer.LoadContainer(500);
    gasContainer.UnloadContainer(300);
    gasContainer.UnloadContainer(390); // error expcted
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"Caught expected exception: {e.Message}");
}

try
{
    CoolerContainer coolerContainer = new CoolerContainer(50, 180, 60, 140, 600, "Bananas", 13.5);
    coolerContainer.LoadContainer(500);
    coolerContainer.LoadContainer(100); // error expcted
}
catch (Exception e)
{
    Console.WriteLine($"Caught expected exception: {e.Message}");
}

try
{
    Ship ship = new Ship(20, 3, 1000);
    ship.AddContainer(new LiquidContainer(100, 200, 50, 150, 1000, false));
    ship.AddContainer(new GasContainer(200, 250, 100, 200, 800, false, 10));
    ship.AddContainer(new CoolerContainer(50, 180, 60, 140, 600, "Cheese", 7.5));
    ship.AddContainer(new LiquidContainer(300, 200, 50, 150, 1000, true)); // // error expcted
}
catch (InvalidOperationException e)
{
    Console.WriteLine($"Caught expected exception: {e.Message}");
}