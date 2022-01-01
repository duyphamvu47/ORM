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
        string BuildUpdate();
        string BuildDelete();
        string BuildSelect();
    }
}
