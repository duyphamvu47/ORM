using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
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
            var dm = new DynamicMethod((string.Format("Dictionary{0}", Guid.NewGuid())), typeof(Dictionary<string, object>), new[] { typeof(object) }, type, true);
            ILGenerator il = dm.GetILGenerator();
            il.DeclareLocal(typeof(Dictionary<string, object>));
            il.Emit(OpCodes.Nop);
            il.Emit(OpCodes.Newobj, typeof(Dictionary<string, object>).GetConstructor(Type.EmptyTypes));
            il.Emit(OpCodes.Stloc_0);

            foreach (var item in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                string columnName = item.Name;
                il.Emit(OpCodes.Nop);
                il.Emit(OpCodes.Ldloc_0);
                il.Emit(OpCodes.Ldstr, columnName);
                il.Emit(OpCodes.Ldarg_0);
                il.Emit(OpCodes.Callvirt, item.GetGetMethod());
                if (item.PropertyType.IsValueType)
                {
                    il.Emit(OpCodes.Box, item.PropertyType);
                }
                il.Emit(OpCodes.Callvirt, typeof(Dictionary<string, object>).GetMethod("Add"));
            }
            il.Emit(OpCodes.Nop);

            il.Emit(OpCodes.Ldloc_0);
            il.Emit(OpCodes.Ret);
            var func = (Func<object, Dictionary<string, object>>)dm.CreateDelegate(typeof(Func<object, Dictionary<string, object>>));
            return func;
        }
    }
}
