public abstract class Character
{
    public string Name { get; set; }
    public int Health { get; private set; }
    public int Level { get; private set; }
    
    public Character(string name, int health, int level)
    {
        Name = name;
        Health = health;
        Level = level;
    }
    
    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
        Console.WriteLine($"{Name} took {damage} damage, remaining health: {Health}");
    }
    
    public void Heal(int amount)
    {
        Health += amount;
        Console.WriteLine($"{Name} healed for {amount}, new health: {Health}");
    }
    
    public void LevelUp()
    {
        Level++;
        Console.WriteLine($"{Name} leveled up! New level: {Level}");
    }
}