using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class EntityMapper
    {
        public Type EntityType { get; internal set; }
        public String TableName { get; set; }
        public List<ColumnMapper> EntityColumns;
        public List<ColumnMapper> PrimaryKeys { get; set;}  
    }
}
