public class Inventory
{
    private List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
        Console.WriteLine($"Added {item.Name} to inventory.");
    }

    public void RemoveItem(Item item)
    {
        if (items.Remove(item))
        {
            Console.WriteLine($"Removed {item.Name} from inventory.");
        }
        else
        {
            Console.WriteLine($"{item.Name} not found in inventory.");
        }
    }

    public bool HasItem(Item item)
    {
        return items.Contains(item);
    }
}