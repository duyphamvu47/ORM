using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ORM
{
    public class DbReader
    {
        private IDataReader reader;

        /*-------------------------------------------------------*/


        public DbReader(IDataReader reader) {

        }


        public T GetValue<T>(int index){
            return default(T);
        }
    }
}
