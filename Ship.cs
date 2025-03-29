namespace Cwiczenia2;

public class Ship
{
    
    public double MaxSpeed { get; }
    public int MaxContainerCount { get; }
    public double MaxTotalWeightKg { get; }
    public List<Container> Containers { get; private set; }
    
    public Ship(double maxSpeed, int maxContainerCount, double maxTotalWeightKg)
    {
        MaxSpeed = maxSpeed;
        MaxContainerCount = maxContainerCount;
        MaxTotalWeightKg = maxTotalWeightKg;
        Containers = new List<Container>();
    }
    
    public void AddContainer(Container container)
    {
        if (Containers.Count >= MaxContainerCount)
        {
            throw new InvalidOperationException($"Cannot add container {container.serialNumber}. Ship has reached maximum container capacity ({MaxContainerCount})");
        }

        double potentialWeight = GetCurrentWeightKg() + container.GetTotalMass();
        if (potentialWeight > MaxTotalWeightKg)
        {
            throw new InvalidOperationException($"Cannot add container {container.serialNumber}. Exceeds maximum ship weight capacity ({MaxTotalWeightKg}kg). Adding it would reach {potentialWeight}kg");
        }

        Containers.Add(container);
        Console.WriteLine($"Container {container.serialNumber} loaded onto ship.");
    }
    
    public void AddContainers(List<Container> containersToAdd)
    {
        if (Containers.Count + containersToAdd.Count > MaxContainerCount)
        {
            Console.WriteLine($"Warning: Not all containers might fit due to count limit.");
        }
        double weightToAdd = containersToAdd.Sum(c => c.GetTotalMass());
        if (GetCurrentWeightKg() + weightToAdd > MaxTotalWeightKg)
        {
            Console.WriteLine($"Warning: Not all containers might fit due to weight limit.");
        }


        int addedCount = 0;
        foreach (var container in containersToAdd)
        {
            try
            {
                AddContainer(container);
                addedCount++;
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Failed to load container {container.serialNumber} onto ship: {ex.Message}. Stopping loading of list.");
                break;
            }
        }
        Console.WriteLine($"Successfully loaded {addedCount} out of {containersToAdd.Count} containers onto the ship.");
    }
    
    public bool RemoveContainer(string serialNumber)
    {
        Container containerToRemove = Containers.FirstOrDefault(c => c.serialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase));
        if (containerToRemove != null)
        {
            Containers.Remove(containerToRemove);
            Console.WriteLine($"Container {serialNumber} removed from ship.");
            return true;
        }
        else
        {
            Console.WriteLine($"Container {serialNumber} not found on ship.");
            return false;
        }
    }
    
    public double GetCurrentWeightKg()
    {
        double currentWeight = 0;
        
        foreach (var container in Containers)
        {
            currentWeight += container.GetTotalMass();
        }
        
        return currentWeight;
    }
    
    public void ReplaceContainer(string serialNumberToReplace, Container newContainer)
    {
        Container containerToRemove = GetContainer(serialNumberToReplace);

        if (containerToRemove == null)
        {
            throw new InvalidOperationException($"Container to replace ({serialNumberToReplace}) not found on the ship.");
        }
        
        double potentialWeight = GetCurrentWeightKg() - containerToRemove.GetTotalMass() + newContainer.GetTotalMass();
        if (potentialWeight > MaxTotalWeightKg) // checking if it will fit
        {
            throw new InvalidOperationException($"Cannot replace with {newContainer.serialNumber}. Exceeds maximum ship weight capacity ({MaxTotalWeightKg}kg). Replacing would reach {potentialWeight}kg");
        }
        
        int index = Containers.IndexOf(containerToRemove);
        Containers.RemoveAt(index); 
        Containers.Insert(index, newContainer); // insert new at same spot

        Console.WriteLine($"Container {serialNumberToReplace} replaced with {newContainer.serialNumber} on ship");
    }
    
    public static void TransferContainer(Ship sourceShip, Ship destinationShip, string serialNumber)
    {
        Container containerToMove = sourceShip.GetContainer(serialNumber);
        if (containerToMove == null)
        {
            throw new InvalidOperationException($"Container {serialNumber} not found on source ship");
        }
        
        if (destinationShip.Containers.Count >= destinationShip.MaxContainerCount)
        {
            throw new InvalidOperationException($"Destination ship has reached maximum container capacity ({destinationShip.MaxContainerCount})");
        }
        
        if (destinationShip.GetCurrentWeightKg() + containerToMove.GetTotalMass() > destinationShip.MaxTotalWeightKg)
        {
            throw new InvalidOperationException($"Adding container {serialNumber} exceeds destination ship's maximum weight capacity ({destinationShip.MaxTotalWeightKg}kg)");
        }
        
        sourceShip.Containers.Remove(containerToMove); // remove from source
        destinationShip.Containers.Add(containerToMove); // Add to destination

        Console.WriteLine($"Container {serialNumber} transferred from source ship to destination ship");
    }
    
    public Container GetContainer(string serialNumber)
    {
        return Containers.FirstOrDefault(c => c.serialNumber.Equals(serialNumber, StringComparison.OrdinalIgnoreCase));
    }
    
    public void PrintShipInfo()
    {
        Console.WriteLine($"Ship Info:");
        Console.WriteLine($"Max Speed: {MaxSpeed} knots");
        Console.WriteLine($"Max Containers: {MaxContainerCount}");
        // Console.WriteLine($"Max Weight: {MaxTotalWeightKg / 1000} tones ({MaxTotalWeightKg} kg)"); 
        Console.WriteLine($"Current Containers: {Containers.Count}");
        Console.WriteLine($"Current Wegiht: {GetCurrentWeightKg() / 1000} tones ({GetCurrentWeightKg()} kg)");
    }
}