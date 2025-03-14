using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Warrior warrior = new Warrior("Conan");
        Mage mage = new Mage("Gandalf");
        
        HealthPotion potion = new HealthPotion();
        
        Inventory inventory = new Inventory();
        inventory.AddItem(potion);
        
        warrior.TakeDamage(30);
        potion.Use(warrior);
        
        warrior.LevelUp();
        
        inventory.RemoveItem(potion);
    }
}
