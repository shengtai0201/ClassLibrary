﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Shengtai
{
    public abstract class Repository<TConnection, TCommand, TParameter, TContext> : ICurrentUser
        where TConnection : DbConnection, new()
        where TCommand : DbCommand, new()
        where TParameter : DbParameter, new()
        where TContext : DbContext
    {
        private readonly string connectionStringName;
        protected TContext DbContext { get; private set; }
        //protected ILogger Logger { get; private set; }

        public IPrincipal CurrentUser { protected get; set; }

        protected Repository(string connectionStringName)
        {
            this.connectionStringName = connectionStringName;
            this.connectionString = string.Empty;

            //if (this.DbContext == null)
            //    this.DbContext = DependencyResolver.Current.GetService<TContext>();

            //if (this.Logger == null)
            //    this.Logger = DependencyResolver.Current.GetService<ILogger>();
        }

        //protected DataSet GetDataSet(string selectCommandText, params SqlParameter[] values)
        //{
        //    var selectConnection = new SqlConnection(this.ConnectionString);
        //    selectConnection.Open();

        //    var dataAdapter = new SqlDataAdapter(selectCommandText, selectConnection);

        //    if (values != null)
        //        dataAdapter.SelectCommand.Parameters.AddRange(values);

        //    var dataSet = new DataSet();
        //    dataAdapter.Fill(dataSet);

        //    dataAdapter.Dispose();
        //    selectConnection.Close();
        //    selectConnection.Dispose();

        //    return dataSet;
        //}

        //protected void ReadData(Action<SqlDataReader> dataReaderAction, string cmdText, params SqlParameter[] values)
        protected void ReadData(Action<DbDataReader> dataReaderAction, string cmdText, params TParameter[] values)
        {
            //var selectConnection = new SqlConnection(this.ConnectionString);
            var selectConnection = Activator.CreateInstance(typeof(TConnection), this.ConnectionString) as DbConnection;
            selectConnection.Open();

            //var command = new SqlCommand(cmdText, selectConnection);
            var command = Activator.CreateInstance(typeof(TCommand), cmdText, selectConnection) as DbCommand;

            if (values != null)
                command.Parameters.AddRange(values);

            //SqlDataReader dataReader = command.ExecuteReader();
            DbDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
                dataReaderAction(dataReader);

            dataReader.Close();
            command.Dispose();
            selectConnection.Close();
            selectConnection.Dispose();
        }

        //protected void ReadDataSingle(Action<SqlDataReader> dataReaderAction, string cmdText, params SqlParameter[] values)
        protected void ReadDataSingle(Action<DbDataReader> dataReaderAction, string cmdText, params TParameter[] values)
        {
            //var selectConnection = new SqlConnection(this.ConnectionString);
            var selectConnection = Activator.CreateInstance(typeof(TConnection), this.ConnectionString) as DbConnection;
            selectConnection.Open();

            //var command = new SqlCommand(cmdText, selectConnection);
            var command = Activator.CreateInstance(typeof(TCommand), cmdText, selectConnection) as DbCommand;

            if (values != null)
                command.Parameters.AddRange(values);

            //SqlDataReader dataReader = command.ExecuteReader();
            DbDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
                dataReaderAction(dataReader);

            dataReader.Close();
            command.Dispose();
            selectConnection.Close();
            selectConnection.Dispose();
        }

        //protected object ExecuteScalar(string cmdText, params SqlParameter[] values)
        protected object ExecuteScalar(string cmdText, params TParameter[] values)
        {
            //var selectConnection = new SqlConnection(this.ConnectionString);
            var selectConnection = Activator.CreateInstance(typeof(TConnection), this.ConnectionString) as DbConnection;
            selectConnection.Open();

            //var command = new SqlCommand(cmdText, selectConnection);
            var command = Activator.CreateInstance(typeof(TCommand), cmdText, selectConnection) as DbCommand;

            if (values != null)
                command.Parameters.AddRange(values);

            object result = command.ExecuteScalar();

            command.Dispose();
            selectConnection.Close();
            selectConnection.Dispose();

            return result;
        }

        //protected async Task<object> ExecuteScalarAsync(string cmdText, params SqlParameter[] values)
        protected async Task<object> ExecuteScalarAsync(string cmdText, params TParameter[] values)
        {
            //var selectConnection = new SqlConnection(this.ConnectionString);
            var selectConnection = Activator.CreateInstance(typeof(TConnection), this.ConnectionString) as DbConnection;
            selectConnection.Open();

            //var command = new SqlCommand(cmdText, selectConnection);
            var command = Activator.CreateInstance(typeof(TCommand), cmdText, selectConnection) as DbCommand;

            if (values != null)
                command.Parameters.AddRange(values);

            object result = await command.ExecuteScalarAsync();

            command.Dispose();
            selectConnection.Close();
            selectConnection.Dispose();

            return result;
        }

        //protected int ExecuteNonQuery(string cmdText, params SqlParameter[] values)
        protected int ExecuteNonQuery(string cmdText, params TParameter[] values)
        {
            //var selectConnection = new SqlConnection(this.ConnectionString);
            var selectConnection = Activator.CreateInstance(typeof(TConnection), this.ConnectionString) as DbConnection;
            selectConnection.Open();

            //var command = new SqlCommand(cmdText, selectConnection);
            var command = Activator.CreateInstance(typeof(TCommand), cmdText, selectConnection) as DbCommand;

            if (values != null)
                command.Parameters.AddRange(values);

            int result = command.ExecuteNonQuery();

            command.Dispose();
            selectConnection.Close();
            selectConnection.Dispose();

            return result;
        }

        protected int ConvertToInt(object value)
        {
            if (value == DBNull.Value)
                return 0;

            return Convert.ToInt32(value);
        }

        protected bool ConvertToBoolean(object value)
        {
            if (value == DBNull.Value)
                return false;

            return Convert.ToBoolean(value);
        }

        protected string DbToString(object value)
        {
            if (value == DBNull.Value)
                return null;
            else
                return value.ToString();
        }

        protected bool? DbToBoolean(object value)
        {
            if (value == DBNull.Value)
                return null;
            else
                return Convert.ToBoolean(value);
        }

        protected string GetDayOfWeekName(DateTime dateTime)
        {
            var provider = new System.Globalization.CultureInfo(name: "zh-TW");
            System.Globalization.DateTimeFormatInfo info = System.Globalization.DateTimeFormatInfo.GetInstance(provider);
            string fullName = info.DayNames[(byte)dateTime.DayOfWeek];

            return fullName.Substring(startIndex: 2, length: 1);
        }

        private string connectionString;
        protected string ConnectionString
        {
            get
            {
                //if (string.IsNullOrEmpty(this.connectionString))
                //    this.connectionString = WebConfigurationManager.ConnectionStrings[this.connectionStringName].ConnectionString;

                return this.connectionString;
            }
        }
    }
}