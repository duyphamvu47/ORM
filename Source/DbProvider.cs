using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ORM
{
    public class DbProvider
    {
        private IDbConnection conntion;


        /*-------------------------------------------------------------------*/

        public DbProvider(IDbConnection conntion)
        {
   
        }

        public virtual int ExecuteNonQuery()
        {
            return 0;
        }

        public IDataReader ExecuteReader(String sqlString, Dictionary<string, object> parameters)
        {
            return null;
        }
    }
}
