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
    public abstract class Repository<TConnection, TCommand, TParameter, TDataAdapter, TDbContext>
        where TConnection : DbConnection, new()
        where TCommand : DbCommand, new()
        where TParameter : DbParameter, new()
        where TDataAdapter : DbDataAdapter, new()
        where TDbContext : DbContext
    {
        protected TDbContext DbContext { get; private set; }
        protected IAppSettings AppSettings { get; private set; }

        protected Repository(IAppSettings appSettings, TDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.AppSettings = appSettings;
        }

        protected T ExecuteScalar<T>(string cmdText, params TParameter[] values)
        {
            TConnection connection = Activator.CreateInstance(typeof(TConnection), this.AppSettings.DefaultConnection) as TConnection;
            connection.Open();

            TCommand command = Activator.CreateInstance(typeof(TCommand), cmdText, connection) as TCommand;
            if (values != null)
                command.Parameters.AddRange(values);

            var value = command.ExecuteScalar();
            var result = (T)Convert.ChangeType(value, typeof(T));

            command.Dispose();
            connection.Close();
            connection.Dispose();

            return result;
        }

        protected void ExecuteReader(Action<DbDataReader> dataReaderAction, string cmdText, params TParameter[] values)
        {
            TConnection connection = Activator.CreateInstance(typeof(TConnection), this.AppSettings.DefaultConnection) as TConnection;
            connection.Open();

            TCommand command = Activator.CreateInstance(typeof(TCommand), cmdText, connection) as TCommand;
            if (values != null)
                command.Parameters.AddRange(values);

            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
                dataReaderAction(dataReader);

            dataReader.Close();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        protected DataSet GetDataSet(string selectCommandText, params TParameter[] values)
        {
            TConnection selectConnection = Activator.CreateInstance(typeof(TConnection), this.AppSettings.DefaultConnection) as TConnection;
            selectConnection.Open();

            TDataAdapter dataAdapter = Activator.CreateInstance(typeof(TDataAdapter), selectCommandText, selectConnection) as TDataAdapter;
            if (values != null)
                dataAdapter.SelectCommand.Parameters.AddRange(values);

            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);

            dataAdapter.Dispose();
            selectConnection.Close();
            selectConnection.Dispose();

            return dataSet;
        }
    }
}
