using System;

namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            //insertEquipment();
            //updateEquipment();
            //deleteEquipment();
            readAllEquipment();

            Console.WriteLine();
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();
        }

        static void readAllEquipment()
        {
            DbContext context = new SuperHeroAppContext();
            var list = context.readAll<equipment>(string.Empty, string.Empty);

            foreach (var item in list)
            {
                Console.WriteLine(item.id + "," + item.name + "," + item.type);
            }

            Console.WriteLine(list.Count);
        }

        static void insertEquipment()
        {
            DbContext context = new SuperHeroAppContext();

            var newEquip = new equipment()
            {
                name = "Quan OOP",
                type = 1,
                HeroId = 1,
            };

            context.insert<equipment>(newEquip);
        }

        static void updateEquipment()
        {
            DbContext context = new SuperHeroAppContext();
            var list = context.readAll<equipment>(string.Empty, string.Empty);

            if (list.Count > 0)
            {
                var equip = list[list.Count - 1];
                equip.name = "Hello new update";

                context.update<equipment>(equip);
            }
        }

        static void deleteEquipment()
        {
            DbContext context = new SuperHeroAppContext();
            var list = context.readAll<equipment>(string.Empty, string.Empty);

            if (list.Count > 0)
            {
                var equip = list[list.Count - 1];

                context.delete<equipment>(equip);
            }
        }
    }
}
