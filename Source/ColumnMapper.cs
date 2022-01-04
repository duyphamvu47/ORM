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
        public string ColumnName { get; internal set; }
        public PropertyInfo PropInfo { get; set; }
        public bool IsPrimaryKey { get; set; }
        public bool IsDbAutoGenerate { get; set; }
    }
}
