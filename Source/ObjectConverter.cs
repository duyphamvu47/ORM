using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class ObjectConverter
    {
        private static Dictionary<Type, Func<object, Dictionary<string, object>>> dictionaryCache = new Dictionary<Type, Func<object, Dictionary<string, object>>>();
        public static Dictionary<Type, Delegate> typeTypeDeserializers = new Dictionary<Type, Delegate>();


        /*--------------------------------------------------------------------------------------*/

        public static Func<DbReader, T> GetTypeDeserializer<T>(EntityMapper entityMappers)
        {
            return null;
        }

        public static Dictionary<string, object> ConvertFromObject(object obj)
        {
            return null;
        }

        public static Func<object, Dictionary<string, object>> CreateDictionaryGenerator(Type type)
        {
            return null;
        }
    }
}
