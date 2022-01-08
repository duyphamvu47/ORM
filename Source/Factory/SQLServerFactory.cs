using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.DbConfig;
using ORM.SqlBuilder;

namespace ORM.Factory
{
    public class SQLServerFactory: IDbFactory
    {
        public IDbConfigBuilder CreateConfigBuilder()
        {
            return new SQLServerConfigBuilder();
        }
        public ISqlStringBuilder CreateSqlStringBuilder()
        {
            return new SQLServerStringBuilder();
        }
    }
}
