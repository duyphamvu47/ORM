using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class SQLServerStringBuilder : ISqlStringBuilder
    {
        public string BuildInsert(EntityMapper entityMapper, List<string> columnNames)
        {
            StringBuilder sqlString = new StringBuilder();
            sqlString.Append("INSERT INTO ").Append(entityMapper.TableName).Append(" (");

            //Convert columnNames to string
            var columns = entityMapper.EntityColumns
                .Where(m => m.IsDbGenerated == false && 
                            columnNames.Contains(m.ColumnName, StringComparer.OrdinalIgnoreCase))
                            .ToArray();

            for (int i = 0; i < columns.Length; i++)
            {
                var member = columns[i];
                if (i > 0)
                    sqlString.Append(", ");

                sqlString.Append(member.ColumnName);
            }

            //Pass param
            sqlString.Append(") VALUES (");
            for (int i = 0; i < columns.Length; i++)
            {
                var member = columns[i];
                if (i > 0)
                    sqlString.Append(", ");

                sqlString.Append("@").Append(member.ColumnName);
            }

            sqlString.Append(")");

            return sqlString.ToString();
        }

        public string BuildSelect(EntityMapper entityMapper, string conditions, string orderBy)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("SELECT ");
            // Build selected column
            for (int i = 0; i < entityMapper.EntityColumns.Count; i++)
            {
                var member = entityMapper.EntityColumns[i];
                if (i > 0)
                {
                    sqlStr.Append(", ");
                }

                sqlStr.Append(member.ColumnName);
            }

            sqlStr.Append(" FROM ").Append(entityMapper.TableName);

            if (!string.IsNullOrEmpty(conditions))
            {
                sqlStr.Append(" WHERE ").Append(conditions);
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                sqlStr.Append(" Order By ").Append(orderBy);
            }

            return sqlStr.ToString();
        }

        public string BuildUpdate(EntityMapper entityMapper, List<string> updateColumns, List<string> whereConditions)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("UPDATE ").Append(entityMapper.TableName).Append(" SET ");

            var columns = entityMapper.EntityColumns.Where(m => 
                            updateColumns.Contains(m.ColumnName, StringComparer.OrdinalIgnoreCase))
                            .ToArray();

            var conditions = entityMapper.EntityColumns.Where(m => 
                            whereConditions.Contains(m.ColumnName, StringComparer.OrdinalIgnoreCase))
                            .ToArray();

            // Build SET string
            for (int i = 0; i < columns.Length; i++)
            {
                var member = columns[i];
                if (i > 0)
                {
                    sqlStr.Append(", ");
                }

                sqlStr.Append(member.ColumnName).Append("=").Append("@").Append(member.ColumnName);
            }

            // Build WHERE string + pass param
            sqlStr.Append(" WHERE ");
            for (int i = 0; i < conditions.Length; i++)
            {
                var member = conditions[i];
                if (i > 0)
                {
                    sqlStr.Append(", ");
                }

                sqlStr.Append(member.ColumnName).Append("=").Append("@p").Append(member.ColumnName);
            }

            return sqlStr.ToString();
        }

        public string BuildDelete(EntityMapper entityMapper, List<string> whereConditions)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("DELETE FROM ").Append(entityMapper.TableName).Append(" WHERE ");

            var conditions = entityMapper.EntityColumns.Where(m => 
                                whereConditions.Contains(m.ColumnName, StringComparer.OrdinalIgnoreCase))
                                .ToArray();

            // Pass param
            for (int i = 0; i < conditions.Length; i++)
            {
                var member = conditions[i];
                if (i > 0)
                {
                    sqlStr.Append(", ");
                }

                sqlStr.Append(member.ColumnName).Append("=").Append("@").Append(member.ColumnName);
            }

            return sqlStr.ToString();
        }
    }
}
