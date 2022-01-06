using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class SQLServerFactory: IDbFactory
    {
        public IDbConfigBuilder CreateConfigBuilder()
        {
            return null;
        }
        public ISqlStringBuilder CreateSqlStringBuilder()
        {
            return new SQLServerStringBuilder();
        }
    }
}
