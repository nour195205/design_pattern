using System;
using System.Collections.Generic;

namespace DesignPatterns.Prototype
{
    public class GameCharacter
    {
        public string Name { get; set; }
        public Equipment Gear { get; set; }

        public GameCharacter(string name, Equipment gear)
        {
            Name = name;
            Gear = gear;
        }

        public GameCharacter ShallowClone()
        {
            return (GameCharacter)this.MemberwiseClone();
        }

        public GameCharacter DeepClone()
        {
            GameCharacter clonedCharacter = (GameCharacter)this.MemberwiseClone();
            clonedCharacter.Gear = this.Gear.Clone();
            return clonedCharacter;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Gear: {Gear}";
        }
    }

    public class Equipment
    {
        public string Weapon { get; set; }
        public int Durability { get; set; }

        public Equipment(string weapon, int durability)
        {
            Weapon = weapon;
            Durability = durability;
        }
        public Equipment Clone()
        {
            return new Equipment(Weapon, Durability);
        }
        public override string ToString()
        {
            return $"{Weapon} (Durability: {Durability})";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var original = new GameCharacter(
                name: "Warrior",
                gear: new Equipment("Sword", 100)
            );

            var shallowCopy = original.ShallowClone();
            shallowCopy.Name = "Shallow Hero";
            shallowCopy.Gear.Durability = 80;

            Console.WriteLine("--- Shallow Copy ---");
            Console.WriteLine($"Original: {original}");
            Console.WriteLine($"Shallow : {shallowCopy}");

            var deepCopy = original.DeepClone();
            deepCopy.Name = "Deep Hero";
            deepCopy.Gear.Durability = 60;

            Console.WriteLine("\n--- Deep Copy ---");
            Console.WriteLine($"Original: {original}");
            Console.WriteLine($"Deep    : {deepCopy}");
        }
    }
}
