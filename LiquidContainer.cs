namespace Cwiczenia2;

public class LiquidContainer : Container
{
    private bool _dangerousPayload;

    public LiquidContainer(int payloadMass, int height, int tareWeight, int depth,
        int maxLoadCapacity, bool dangerousPayload) : base(payloadMass, height, tareWeight, depth, maxLoadCapacity, "L")
    {
        _dangerousPayload = dangerousPayload;
    }

    public void LoadContainer(int loadingMass)
    {
        base.LoadContainer(loadingMass, _dangerousPayload);
    }
}