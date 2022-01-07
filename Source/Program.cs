using System;

namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            DbContext context = new SuperHeroAppContext();
            var list = context.readAll<equipment>(string.Empty, string.Empty);

            foreach (var item in list)
            {
                Console.WriteLine(item.id + "," + item.name + "," + item.type);
            }

            Console.WriteLine(list.Count);
            Console.WriteLine();
            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();
        }
    }
}
