using ORM.QueryUtils.QueryUtilsEnum;
using System;
using ORM.ProgramDomain;
using ORM.QueryUtils;

namespace ORM
{
    class Program
    {
        static void Main(string[] args)
        {
            //insertEquipment();
            //updateEquipment();
            //deleteEquipment();
            //readAllEquipment();
            readEquipmentsWithWhere();

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
                name = "Dai lung OOP",
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

        static void readEquipmentsWithWhere()
        {
            IQueryBuilder queryWhere1 = new SqlServerQueryBuilder();
            queryWhere1.where("Type", WhereOperatorEnum.Equals, 2);

            IQueryBuilder queryWhere2 = new SqlServerQueryBuilder();
            queryWhere2.where("Id", WhereOperatorEnum.GreaterThan, 4);

            IQueryBuilder queryWhere3 = new SqlServerQueryBuilder();
            queryWhere3.whereIn("HeroId", new int[] { 1, 3 });

            string whereString = queryWhere1.whereAnd(queryWhere2)
                .whereOr(queryWhere3)
                .ToWhereString();

            DbContext context = new SuperHeroAppContext();
            var list = context.readAll<equipment>(whereString, "Name desc");

            Console.WriteLine("ID, Name, Type, HeroId");
            foreach (var item in list)
            {
                Console.WriteLine(item.id + "," + item.name + "," + item.type + "," + item.HeroId);
            }
        }
    }
}
