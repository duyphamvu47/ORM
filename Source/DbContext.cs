﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class DbContext
    {
        protected ISqlStringBuilder _sqlStringBuilder;
        protected DbManager _dbProvider;
        protected IDbFactory _dbFactory;

        /*-------------------------------------------*/

        public virtual void onConfiguareDB()
        {

        }

        /** 
         * input object as type of entity
         * Return int - the number of row inserted
         */
        public int insert<T>(T data)
        {
            var entityMapper = DataMapper.Get<T>();
            var valOfParams = ObjectConverter.ConvertDictionaryFromObject(data);
            var sql = _sqlStringBuilder.BuildInsert(entityMapper, valOfParams.Keys.ToList());
            return _dbProvider.ExecuteNonQuery(sql, valOfParams);
        }

        public int update<T>(T data)
        {
            var entityMapper = DataMapper.Get<T>();
            var valOfParams = ObjectConverter.ConvertDictionaryFromObject(data);
            var columns = entityMapper.EntityColumns.Where(col => col.IsDbAutoGenerate == false && col.IsPrimaryKey == false).Select(c => c.ColumnName).ToArray();
            var updateColumns = new Dictionary<string, object>();
            var whereColumns = new Dictionary<string, object>();

            foreach (var item in valOfParams)
            {
                if (columns.Contains(item.Key))
                    updateColumns.Add(item.Key, item.Value);
                if (entityMapper.PrimaryKeys.All(pk => pk.ColumnName == item.Key))
                    whereColumns.Add(item.Key, item.Value);
            }

            var sql = _sqlStringBuilder.BuildUpdate(entityMapper, updateColumns.Keys.ToList(), whereColumns.Keys.ToList());

            return _dbProvider.ExecuteNonQuery(sql, valOfParams);
        }

        /** 
         * input list of primary key values - 1 table can have multiple primary key
         * Return int - the number of row inserted
         */
        public int deleteByID<T>(Dictionary<string, object> tablePrimaryKeyVals)
        {
            var entityMapper = DataMapper.Get<T>();

            if (tablePrimaryKeyVals.Count() != entityMapper.PrimaryKeys.Count)
                throw new ArgumentException("primary keys values is inconsistence with primary keys in table");

            var parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
            for (int i = 0; i < entityMapper.PrimaryKeys.Count; i++)
            {
                var columnName = entityMapper.PrimaryKeys[i].ColumnName;
                parameters.Add(columnName, tablePrimaryKeyVals.GetValueOrDefault(columnName));
            }

            var sql = _sqlStringBuilder.BuildDelete(entityMapper, parameters.Keys.ToList());

            return _dbProvider.ExecuteNonQuery(sql, parameters);
        }

        public void read()
        {

        }
    }
}
