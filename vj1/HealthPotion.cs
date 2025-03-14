public class HealthPotion : Item, IUsable
{
    private int HealAmount;

    public HealthPotion() : base("Health Potion")
    {
        HealAmount = 20;
    }

    public void Use(Character character)
    {
        character.Heal(HealAmount);
        Console.WriteLine($"{character.Name} used {Name} and restored {HealAmount} health.");
    }
}