namespace Cwiczenia2;

public abstract class Container : IHazardNotifier
{
    public int payloadMass { get; set; }
    protected int tareWeight { get; set; } // weight of the container
    protected int height { get; set; }
    protected int depth { get; set; }
    public string serialNumber { get; private set; }
    protected int maxLoadCapacity { get; set; }

    public Container(int payloadMass, int height, int tareWeight, int depth, int maxLoadCapacity, string containerType)
    {
        this.payloadMass = payloadMass;
        this.height = height;
        this.tareWeight = tareWeight;
        this.maxLoadCapacity = maxLoadCapacity;
        this.depth = depth;
        this.serialNumber = GenerateSerialNumber(containerType);
    }

    public void NotifyHazard(string containerNumber, string message)
    {
        Console.WriteLine($"[HAZARD] Container: {containerNumber}, Description: {message}");
    }


public void LoadContainer(int loadingMass, bool? dangerousPayload = null)
{
    double maxCapacity;
    if (dangerousPayload.HasValue)
    {
        maxCapacity = dangerousPayload.Value ? 0.5 * maxLoadCapacity : 0.9 * maxLoadCapacity;
    }
    else
    {
        maxCapacity = maxLoadCapacity;
    }

    if (payloadMass + loadingMass > maxCapacity)
    {
        NotifyHazard(serialNumber, $"Cannot load {loadingMass}kg. Current payload is {payloadMass}kg. Exceeds allowed capacity of {maxCapacity}kg ({(dangerousPayload.HasValue ? (dangerousPayload.Value ? 50 : 90) : 100)}% of max {maxLoadCapacity}kg).");
        throw new Exception("OverfillException");
    }
    
    payloadMass += loadingMass;
    Console.WriteLine($"Container {serialNumber} loaded successfully!");
}
    
    public virtual void UnloadContainer(int unloadingMass)
    {
        if (payloadMass - unloadingMass < 0)
        {
            NotifyHazard(serialNumber, $"Cannot unload {unloadingMass}kg. Current payload is only {payloadMass}kg.");
            throw new Exception("UnderfillException");
        }
        
        payloadMass -= unloadingMass; 
        
        Console.WriteLine($"Unloaded {unloadingMass}kg from container {serialNumber}. Remaining payload: {payloadMass}kg.");
    }
    public virtual void EmptyContainer()
    {
        Console.WriteLine($"Emptying container {serialNumber}. Current payload: {payloadMass}kg.");
        
        this.payloadMass = 0;
        Console.WriteLine($"Container {serialNumber} emptied.");
    }

    public int GetTotalMass() => tareWeight + payloadMass;

    public string GenerateSerialNumber(string containerType)
    {
        String uniqueId = Guid.NewGuid().ToString("N").Substring(0, 5);
        return "KON-" + containerType + "-" + uniqueId;
    }
}