using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ORM
{
    public class DbManager
    {
        private IDbConnection connection;

        /*-------------------------------------------------------------------*/

        public DbManager(IDbConnection connection)
        {
            this.connection = connection;
        }

        public virtual int ExecuteNonQuery(string sqlStr, Dictionary<string, object> conditions)
        {
            var isClose = this.connection.State == ConnectionState.Closed ? true : false;
            try
            {
                var command = this.connection.CreateCommand();
                command.CommandText = sqlStr;
                foreach (var condition in conditions)
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = condition.Key;
                    parameter.Value = condition.Value ?? DBNull.Value;
                    command.Parameters.Add(parameter);
                }

                if (isClose) {
                    this.connection.Open();
                }
                return command.ExecuteNonQuery();
            }
            finally
            {
                if (isClose)
                {
                    this.connection.Close();
                }
            }
        }

        public IDataReader ExecuteReader(string sqlStr, Dictionary<string, object> conditions)
        {
            var isClose = this.connection.State == ConnectionState.Closed ? true : false;
            try
            {
                var command = this.connection.CreateCommand();
                command.CommandText = sqlStr;

                if (conditions != null && conditions.Count > 0)
                {
                    foreach (var condition in conditions)
                    {
                        var parameter = command.CreateParameter();
                        parameter.ParameterName = condition.Key;
                        parameter.Value = condition.Value ?? DBNull.Value;
                        command.Parameters.Add(parameter);
                    }
                }

                if (isClose)
                {
                    this.connection.Open();
                }

                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
