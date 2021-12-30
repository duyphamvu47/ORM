using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class SQLServerConfigBuilder : IDbConfigBuilder
    {
        public void addDbAddress(string addr)
        {
            throw new NotImplementedException();
        }

        public void addDbCustom(string custom)
        {
            throw new NotImplementedException();
        }

        public void addDbPasswordString(string pass)
        {
            throw new NotImplementedException();
        }

        public void addDbPort(string port)
        {
            throw new NotImplementedException();
        }

        public DbProvider buildProvider()
        {
            return null;
        }

        public void reset()
        {
            throw new NotImplementedException();
        }
    }
}
