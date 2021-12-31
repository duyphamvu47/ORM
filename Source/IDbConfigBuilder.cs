using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public interface IDbConfigBuilder
    {
        void reset();
        void addDbAddress(string addr);
        void addDbPort(string port);
        void addDbPasswordString (string pass);
        void addDbCustom(string custom);
        DbManager buildProvider();
    }
}
