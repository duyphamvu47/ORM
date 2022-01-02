using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public interface ISqlStringBuilder
    {
        string BuildInsert(EntityMapper entityMapping, List<string> columnNames);
        string BuildUpdate(EntityMapper entityMapping, List<string> updateColumns, List<string> whereColumns);
        string BuildDelete();
        string BuildSelect();
    }
}
