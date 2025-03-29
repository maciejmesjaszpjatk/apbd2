namespace Cwiczenia2;

public interface IHazardNotifier
{
    void NotifyHazard(string containerNumber, string message)
    {
        Console.WriteLine($"[HAZARD] Container: {containerNumber}, Description: {message}");
    }
}
