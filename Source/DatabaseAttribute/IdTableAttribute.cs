using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM.DatabaseAttribute
{
    [AttributeUsage(AttributeTargets.Property)]
    class IdTableAttribute : ColumnAttribute
    {
        public IdTableAttribute(string name) : base(name)
        {

        }
    }
}
