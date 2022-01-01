using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class SQLServerStringBuilder : ISqlStringBuilder
    {
        public string BuildDelete()
        {
            throw new NotImplementedException();
        }

        public string BuildInsert(EntityMapper entityMapping, List<string> columnNames)
        {
            StringBuilder sqlString = new StringBuilder();
            sqlString.Append("INSERT INTO ").Append(entityMapping.TableName).Append(" (");

            //Convert columnNames to string
            var columns = entityMapping.EntityColumns
                .Where(m => m.IsDbGenerated == false && 
                            columnNames.Contains(m.ColumnName, StringComparer.OrdinalIgnoreCase))
                            .ToArray();

            for (int i = 0; i < columns.Length; i++)
            {
                var member = columns[i];
                if (i > 0)
                    sqlString.Append(",");

                sqlString.Append(member.ColumnName);
            }

            //Pass value 
            sqlString.Append(") VALUES (");
            for (int i = 0; i < columns.Length; i++)
            {
                var member = columns[i];
                if (i > 0)
                    sqlString.Append(",");

                sqlString.Append("@").Append(member.ColumnName);
            }

            sqlString.Append(")");

            return sqlString.ToString();
        }

        public string BuildSelect()
        {
            throw new NotImplementedException();
        }

        public string BuildUpdate()
        {
            throw new NotImplementedException();
        }
    }
}
