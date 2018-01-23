using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Shengtai
{
    public abstract class Repository<TConnection, TCommand, TParameter, TDbContext>
        where TConnection : DbConnection, new()
        where TCommand : DbCommand, new()
        where TParameter : DbParameter, new()
        where TDbContext : DbContext
    {
        protected TDbContext DbContext { get; private set; }
        private IAppSettings appSettings;

        protected Repository(IAppSettings appSettings, TDbContext dbContext)
        {
            this.appSettings = appSettings;
            this.DbContext = dbContext;
        }

        protected void Read(Action<DbDataReader> action, string cmdText, params TParameter[] values)
        {
            var connection = Activator.CreateInstance(typeof(TConnection), this.appSettings.DefaultConnection) as TConnection;
            connection.Open();

            var command = Activator.CreateInstance(typeof(TCommand), cmdText, connection) as TCommand;
            if (values != null)
                command.Parameters.AddRange(values);

            DbDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
                action(dataReader);

            dataReader.Close();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        protected void ReadSingle(Action<DbDataReader> action, string cmdText, params TParameter[] values)
        {
            var connection = Activator.CreateInstance(typeof(TConnection), this.appSettings.DefaultConnection) as TConnection;
            connection.Open();

            var command = Activator.CreateInstance(typeof(TCommand), cmdText, connection) as TCommand;
            if (values != null)
                command.Parameters.AddRange(values);

            DbDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
                action(dataReader);

            dataReader.Close();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }
    }
}
