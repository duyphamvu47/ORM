using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.DbConfig;
using ORM.SqlBuilder;

namespace ORM.Factory
{
    public interface IDbFactory
    {
        IDbConfigBuilder CreateConfigBuilder();
        ISqlStringBuilder CreateSqlStringBuilder();
    }
}
