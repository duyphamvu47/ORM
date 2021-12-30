using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class DataMapper
    {
        private Dictionary<Type, EntityMapper> entityMappings = new Dictionary<Type, EntityMapper>();

        /*-------------------------------------------*/

        private EntityMapper Get<T>()
        {
            return null;
        }

        private EntityMapper Get(Type type)
        {
            return null;
        }
    }
}
