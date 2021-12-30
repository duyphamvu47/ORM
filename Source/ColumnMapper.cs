using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ORM
{
    public class ColumnMapper
    {
        public string ColumnName;
        public PropertyInfo PropInfo;
        public bool IsPrimaryKey;
        public bool IsDbGenerated;
    }
}
