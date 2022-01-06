using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class DbContext
    {
        protected ISqlStringBuilder sqlStringBuilder;
        protected DbManager dbProvider;
        protected IDbFactory dbFactory;

        /*-------------------------------------------*/

        public void onConfiguareDB()
        {

        }

        public void insert<T>(T data)
        {

        }

        public void update<T>(T data)
        {

        }

        public void delete<T>(T data)
        {

        }

        public void read()
        {

        }
    }
}
