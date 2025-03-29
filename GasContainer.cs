namespace Cwiczenia2;

public class GasContainer : Container
{
    private bool _dangerousPayload;
    private int _pressure;
    public GasContainer(int payloadMass, int height, int tareWeight, int depth,
        int maxLoadCapacity, bool dangerousPayload, int pressure) : base(payloadMass, height, tareWeight, depth, maxLoadCapacity, "G")
    {
        _dangerousPayload = dangerousPayload;
        _pressure = pressure;
    }
    
    public void LoadContainer(int loadingMass)
    {
        base.LoadContainer(loadingMass, _dangerousPayload);
    }
    
    public override void UnloadContainer(int unloadingMass)
    {
        double fivePercentPayload = payloadMass * 0.05;
        double maxUnloadable = payloadMass - fivePercentPayload;

        if (unloadingMass > maxUnloadable)
        {
            NotifyHazard(serialNumber, $"Unloading {unloadingMass}kg violates the 5% minimum rule. Maximum possible unload is {maxUnloadable}kg.");
            throw new InvalidOperationException("Cannot unload below 5% payload for Gas Container.");
        }
        else
        {
            base.UnloadContainer(unloadingMass);
        }
    }
    
    public override void EmptyContainer()
    {
        double fivePercentPayload = payloadMass * 0.05;
        double amountToUnload = payloadMass - fivePercentPayload;

        if (amountToUnload > 0)
        {
            amountToUnload = Math.Max(0, amountToUnload);
            int unloadInt = (int)Math.Floor(amountToUnload);

            Console.WriteLine($"Emptying gas contaier {serialNumber} to 5% ({fivePercentPayload:F2}kg). Unloading {unloadInt}kg.");
            base.UnloadContainer(unloadInt); 
        }
        else
        {
            Console.WriteLine($"Container {serialNumber} is already at or below 5% payload ({payloadMass}kg). No need for unloading.");
        }
    }
}