using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace ORM
{
    public class DbReader
    {
        private static MethodInfo genericGetValue = typeof(DbReader).GetMethods().Where(c => c.Name == "getValue" && c.IsGenericMethod).First();
        private IDataReader reader;

        /*-------------------------------------------------------*/


        public DbReader(IDataReader reader) {
            this.reader = reader;
        }


        public T getValue<T>(int index){
            object value = reader.GetValue(index);

            if (value == DBNull.Value)
            {
                return default(T);
            }

            return (T)reader.GetValue(index);
        }

        public static MethodInfo getValueMethod(Type type)
        {
            return genericGetValue.MakeGenericMethod(type);
        }
    }
}
