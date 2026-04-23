using System;
using System.Collections.Generic;

namespace DesignPatterns.Prototype
{
    public class GameCharacter
    {
        public string Name { get; set; }
        public List<string> Skills { get; set; }

        public GameCharacter(string name, List<string> skills)
        {
            Name = name;
            Skills = skills;
        }

        // Shallow Copy: بينسخ القيم الأساسية بس، والقوائم بتشير لنفس المكان
        public GameCharacter ShallowClone()
        {
            return (GameCharacter)this.MemberwiseClone();
        }

        // Deep Copy: بينسخ كل حاجة في مكان جديد تماماً
        public GameCharacter DeepClone()
        {
            var clone = (GameCharacter)this.MemberwiseClone();
            clone.Skills = new List<string>(this.Skills); // إنشاء قائمة جديدة تماماً
            return clone;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Skills: {string.Join(", ", Skills)}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var original = new GameCharacter("Warrior", new List<string> { "Fight", "Run" });

            // 1. Shallow Copy
            var shallow = original.ShallowClone();
            shallow.Name = "Shallow Hero";
            shallow.Skills.Add("Jump"); // هيأثر على الأصل!

            Console.WriteLine("--- Shallow Copy Result ---");
            Console.WriteLine($"Original: {original}");
            Console.WriteLine($"Shallow : {shallow}");

            // 2. Deep Copy
            var deep = original.DeepClone();
            deep.Name = "Deep Hero";
            deep.Skills.Add("Fly"); // مش هيأثر على الأصل

            Console.WriteLine("\n--- Deep Copy Result ---");
            Console.WriteLine($"Original: {original}");
            Console.WriteLine($"Deep    : {deep}");
        }
    }
}
