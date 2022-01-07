using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    [Table("Equipments")]
    class equipment
    {
        [IdTable("Id", IsAutoGenerate = true)]
        public int id { get; set; }

        [Column("Name")]
        public string name { get; set; }

        [Column("Type")]
        public int type { get; set; }

        [Column("HeroId")]
        public int HeroId { get; set; }
    }
}
