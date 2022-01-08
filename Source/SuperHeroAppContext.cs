using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ORM.Factory;
using ORM.DbConfig;

namespace ORM
{
    class SuperHeroAppContext : DbContext
    {
        public override void onConfiguareDB()
        {
            base.onConfiguareDB();

            base.changeFactory(new SQLServerFactory());
            IDbFactory factory = base.getFactory();

            base.changeSqlStringBuilder(factory.CreateSqlStringBuilder());

            IDbConfigBuilder builder = factory.CreateConfigBuilder();
            builder.addDbAddress("localhost\\SQLEXPRESS");
            builder.addDbName("SuperheroAppDb");
            builder.addDbCustom("Trusted_Connection=True");
            DbManager provider = builder.buildProvider();

            base.changeProvider(provider);
        }
    }
}
