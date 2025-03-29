namespace Cwiczenia2;

public class CoolerContainer : Container
{
    private string _productType;
    private double _temperature;
    private double _requiredTemperature;
    
    private static Dictionary<string, double> ProductTemperatures = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase)
    {
        {"Bananas", 13.3}, {"Chocolate", 18}, {"Fish", 2}, {"Meat", -15},
        {"Ice cream", -18}, {"Frozen pizza", -30}, {"Cheese", 7.2},
        {"Sausages", 5}, {"Butter", 20.5}, {"Eggs", 19}
    };
    
    public CoolerContainer(int payloadMass, int height, int tareWeight, int depth,
        int maxLoadCapacity, string productType, double containerTemperature)
        : base(payloadMass, height, tareWeight, depth, maxLoadCapacity, "C")
    {
        if (!ProductTemperatures.TryGetValue(productType, out _requiredTemperature))
        {
            throw new ArgumentException($"Inavlid product type specified: {productType}");
        }

        if (containerTemperature < _requiredTemperature)
        {
            throw new ArgumentException($"Container temperature ({containerTemperature}°C) cannot be lower than required temperature for {productType} ({_requiredTemperature}°C)");
        }

        _productType = productType;
        _temperature = containerTemperature;
    }
    
    public void LoadContainer(int loadingMass)
    {
        // System.Console.WriteLine("Child triggered");
        base.LoadContainer(loadingMass, false);
    }
    
}