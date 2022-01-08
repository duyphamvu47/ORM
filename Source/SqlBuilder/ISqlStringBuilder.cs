using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Mapper;

namespace ORM.SqlBuilder
{
    public interface ISqlStringBuilder
    {
        string BuildInsert(EntityMapper entityMapper, List<string> columnNames);
        string BuildUpdate(EntityMapper entityMapper, List<string> updateColumns, List<string> whereConditions);
        string BuildDelete(EntityMapper entityMapper, List<string> whereConditions);
        string BuildSelect(EntityMapper entityMapper, string conditions, string orderBy);
    }
}
