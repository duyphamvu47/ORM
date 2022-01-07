using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class ObjectConverter
    {
        private static Dictionary<Type, Func<object, Dictionary<string, object>>> dictionaryCache = new Dictionary<Type, Func<object, Dictionary<string, object>>>();
        public static Dictionary<Type, Delegate> typeDeserializers = new Dictionary<Type, Delegate>();

        public static Func<DbReader, T> GetTypeDeserializer<T>(EntityMapper entityMapper)
        {
            if (typeDeserializers.ContainsKey(entityMapper.EntityType) == false)
            {
                ParameterExpression parExpr = Expression.Parameter(typeof(DbReader), "r");


                var newExpr = Expression.New(entityMapper.EntityType.GetConstructors().First());

                var method = typeof(DbReader).GetMethods().Where(c => c.Name == "getValue" && c.IsGenericMethod).First();

                List<MemberBinding> memberBindings = new List<MemberBinding>();
                int index = 0;
                foreach (var item in entityMapper.EntityColumns)
                {
                    var callExpr = Expression.Call(parExpr, method.MakeGenericMethod(item.PropInfo.PropertyType), Expression.Constant(index));
                    var memberAssignment = Expression.Bind(item.PropInfo, callExpr);
                    memberBindings.Add(memberAssignment);
                    index++;
                }

                var init = Expression.MemberInit(newExpr, memberBindings);
                Expression<Func<DbReader, T>> expr = (Expression<Func<DbReader, T>>)Expression.Lambda(init, parExpr);

                var func = expr.Compile();
                typeDeserializers.Add(entityMapper.EntityType, func);
            }
            return (Func<DbReader, T>)typeDeserializers[entityMapper.EntityType];
        }

        public static Dictionary<string, object> ConvertDictionaryFromObject(object inputObject)
        {
            if (inputObject is Dictionary<string, object>)
                return (Dictionary<string, object>)inputObject;

            var nameValCol = inputObject as NameValueCollection;
            if (nameValCol != null)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                foreach (string key in nameValCol.Keys)
                    dict.Add(key, nameValCol[key]);
                return dict;
            }

            var hashtable = inputObject as Hashtable;
            if (hashtable != null)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
                foreach (string key in hashtable.Keys.OfType<string>())
                    dict.Add(key, hashtable[key]);
                return dict;
            }
            var objType = inputObject.GetType();
            lock (dictionaryCache)
            {
                Func<object, Dictionary<string, object>> getter;
                if (dictionaryCache.TryGetValue(objType, out getter) == false)
                {
                    getter = CreateDictionaryGenerator(objType);
                    dictionaryCache[objType] = getter;
                }
                var dict = getter(inputObject);
                return dict;
            }
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
