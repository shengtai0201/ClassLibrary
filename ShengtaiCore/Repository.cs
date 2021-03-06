﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Shengtai.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Shengtai
{
    public abstract class Repository<TConnection, TCommand, TParameter, TDataAdapter, TDbContext, TDefaultConnection>
        where TConnection : DbConnection, new()
        where TCommand : DbCommand, new()
        where TParameter : DbParameter, new()
        where TDataAdapter : DbDataAdapter, new()
        where TDbContext : DbContext
        where TDefaultConnection : IDefaultConnection
    {
        protected TDbContext DbContext { get; private set; }
        protected IAppSettings<TDefaultConnection> AppSettings { get; private set; }

        protected Repository(IAppSettings<TDefaultConnection> appSettings, TDbContext dbContext)
        {
            this.DbContext = dbContext;
            this.AppSettings = appSettings;
        }

        protected T ExecuteScalar<T>(string cmdText, params TParameter[] values)
        {
            TConnection connection = Activator.CreateInstance(typeof(TConnection), this.AppSettings.ConnectionStrings.DefaultConnection) as TConnection;
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

        protected async Task<T> ExecuteScalarAsync<T>(string cmdText, params TParameter[] values)
        {
            TConnection connection = Activator.CreateInstance(typeof(TConnection), this.AppSettings.ConnectionStrings.DefaultConnection) as TConnection;
            connection.Open();

            TCommand command = Activator.CreateInstance(typeof(TCommand), cmdText, connection) as TCommand;

            if (values != null)
                command.Parameters.AddRange(values);

            var value = await command.ExecuteScalarAsync();
            var result = (T)Convert.ChangeType(value, typeof(T));

            command.Dispose();
            connection.Close();
            connection.Dispose();

            return result;
        }

        protected void ExecuteReader(Action<DbDataReader> dataReaderAction, string cmdText, params TParameter[] values)
        {
            TConnection connection = Activator.CreateInstance(typeof(TConnection), this.AppSettings.ConnectionStrings.DefaultConnection) as TConnection;
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

        protected IEnumerable<T> ExecuteReader<T>(Func<DbDataReader, T> dataReaderFunc, string cmdText, params TParameter[] values)
        {
            TConnection connection = Activator.CreateInstance(typeof(TConnection), this.AppSettings.ConnectionStrings.DefaultConnection) as TConnection;
            connection.Open();

            TCommand command = Activator.CreateInstance(typeof(TCommand), cmdText, connection) as TCommand;
            if (values != null)
                command.Parameters.AddRange(values);

            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                T data = dataReaderFunc(dataReader);
                yield return data;
            }

            dataReader.Close();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }

        protected DataSet GetDataSet(string selectCommandText, params TParameter[] values)
        {
            TConnection selectConnection = Activator.CreateInstance(typeof(TConnection), this.AppSettings.ConnectionStrings.DefaultConnection) as TConnection;
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
