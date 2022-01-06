using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class DataMapper
    {
        private static Dictionary<Type, EntityMapper> entityMappers = new Dictionary<Type, EntityMapper>();

        public static EntityMapper Get<T>()
        {
            return Get(typeof(T));
        }

        public static EntityMapper Get(Type type)
        {
            EntityMapper _entityMapper;
            if (entityMappers.TryGetValue(type, out _entityMapper) == false)
            {
                _entityMapper = GenerateEntityMapper(type);
                entityMappers.Add(type, _entityMapper);
            }
            return _entityMapper;
        }

        public static EntityMapper GenerateEntityMapper(Type type)
        {
            EntityMapper entityMapper = new EntityMapper()
            {
                EntityType = type
            };
            string tableName = string.Empty;

            var properties = type.GetProperties();

            TableAttribute[] tableAttrs = (TableAttribute[])type.GetCustomAttributes(typeof(TableAttribute), true);
            if (tableAttrs.Length > 0)
            {
                tableName = tableAttrs[0].Name;
            }
            else
            {
                tableName = type.Name;
            }


            List<ColumnMapper> members = new List<ColumnMapper>();
            for (int i = 0; i < properties.Length; i++)
            {
                var propI = properties[i];

                var idAttrs = (IdTableAttribute[])propI.GetCustomAttributes(typeof(IdTableAttribute), true);
                bool isPrimaryKey = idAttrs.Length > 0;

                var attrs = (ColumnAttribute[])propI.GetCustomAttributes(typeof(ColumnAttribute), true);
                if (attrs.Length > 0)
                {
                    var m = new ColumnMapper()
                    {
                        ColumnName = attrs[0].Name,
                        IsDbAutoGenerate = attrs[0].IsAutoGenerate,
                        PropInfo = propI,
                        IsPrimaryKey = isPrimaryKey
                    };
                    members.Add(m);
                }
            }

            entityMapper.TableName = tableName;
            entityMapper.EntityColumns = members;
            entityMapper.PrimaryKeys = members.Where(col => col.IsPrimaryKey).ToList();
            return entityMapper;
        }
    }
}
