using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class SQLServerConfigBuilder : IDbConfigBuilder
    {
        private string address {get; set;}
        private string port { get; set; }
        private string password { get; set; }
        private StringBuilder custom { get; set; }

        public void addDbAddress(string addr)
        {
            this.address = addr;
        }

        public void addDbCustom(string custom)
        {
            if (this.custom.Length > 0)
            {
                this.custom.Append("; ");
            }
            this.custom.Append(custom);
        }

        public void addDbPasswordString(string pass)
        {
            this.password = pass;
        }

        public void addDbPort(string port)
        {
            this.port = port;
        }

        public DbManager buildProvider()
        {
            return null;
        }

        public void reset()
        {
            this.address = null;
            this.port = null;
            this.password = null;
            this.custom = null;
        }
    }
}
