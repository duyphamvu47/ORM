using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM
{
    public class SQLServerConfigBuilder : IDbConfigBuilder
    {
        private string address {get; set;}
        private string instanceName { get; set; }
        private string port { get; set; }
        private string username { get; set; }
        private string password { get; set; }
        private string dbname { get; set; }
        private StringBuilder custom { get; set; } = new StringBuilder();

        public void addDbAddress(string addr)
        {
            this.address = addr;
        }

        public void addDbIntanceName(string name)
        {
            this.instanceName = name;
        }

        public void addDbCustom(string custom)
        {
            if (this.custom.Length > 0)
            {
                this.custom.Append(";");
            }
            this.custom.Append(custom);
        }

        public void addDbUserName(string name)
        {
            this.username = name;
        }

        public void addDbPassword(string pass)
        {
            this.password = pass;
        }

        public void addDbPort(string port)
        {
            this.port = port;
        }

        public void addDbName(string name)
        {
            this.dbname = name;
        }

        public DbManager buildProvider()
        {
            var connectionString = buildConnectionString();
            var connection = createConnection(connectionString);

            return new DbManager(connection);
        }

        private IDbConnection createConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }

        private string buildConnectionString()
        {
            StringBuilder connectionString = new StringBuilder();

            connectionString.Append("Server=");
            if (!string.IsNullOrEmpty(this.address))
            {
                connectionString.Append(this.address);
            } else
            {
                connectionString.Append(".");
            }

            if (!string.IsNullOrEmpty(this.instanceName))
            {
                connectionString.Append("\\");
                connectionString.Append(this.instanceName);
            }

            if (!string.IsNullOrEmpty(this.port))
            {
                connectionString.Append(",");
                connectionString.Append(this.port);
            }

            if (!string.IsNullOrEmpty(this.dbname))
            {
                connectionString.Append(";");
                connectionString.Append("Database=");
                connectionString.Append(this.dbname);
            }

            if (!string.IsNullOrEmpty(this.username))
            {
                connectionString.Append(";");
                connectionString.Append("User Id=");
                connectionString.Append(this.username);
            }

            if (!string.IsNullOrEmpty(this.password))
            {
                connectionString.Append(";");
                connectionString.Append("Password=");
                connectionString.Append(this.password);
            }

            if (this.custom.Length > 0)
            {
                connectionString.Append(";");
                connectionString.Append(this.custom);
            }

            connectionString.Append(";");

            return connectionString.ToString();
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
