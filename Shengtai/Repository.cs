using Microsoft.Owin;
using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Shengtai
{
    public abstract class Repository<TConnection, TCommand, TParameter, TContext> : IService
        where TConnection : DbConnection, new()
        where TCommand : DbCommand, new()
        where TParameter : DbParameter, new()
        where TContext : DbContext
    {
        protected TContext DbContext { get; set; }
        //protected ILogger Logger { get; private set; }

        public IPrincipal CurrentUser { protected get; set; }
        public IOwinContext OwinContext { protected get; set; }

        protected string connectionString;
        protected Repository(TContext context = null, bool setService = false)
        {
            if (context == null)
                this.DbContext = DependencyResolver.Current.GetService<TContext>();
            else
                this.DbContext = context;

            if (string.IsNullOrEmpty(this.connectionString) && this.DbContext != null)
                this.connectionString = this.DbContext.Database.Connection.ConnectionString;

            //if (this.Logger == null)
            //    this.Logger = DependencyResolver.Current.GetService<ILogger>();

            if (setService)
            {
                this.CurrentUser = System.Web.HttpContext.Current.User;
                this.OwinContext = System.Web.HttpContext.Current.GetOwinContext();
            }
        }

        protected void ReadData(Action<DbDataReader> dataReaderAction, string cmdText, params TParameter[] values)
        {
            var selectConnection = Activator.CreateInstance(typeof(TConnection), this.connectionString) as TConnection;
            selectConnection.Open();

            var command = Activator.CreateInstance(typeof(TCommand), cmdText, selectConnection) as TCommand;

            if (values != null)
                command.Parameters.AddRange(values);

            DbDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
                dataReaderAction(dataReader);

            dataReader.Close();
            command.Dispose();
            selectConnection.Close();
            selectConnection.Dispose();
        }

        protected void ReadDataSingle(Action<DbDataReader> dataReaderAction, string cmdText, params TParameter[] values)
        {
            var selectConnection = Activator.CreateInstance(typeof(TConnection), this.connectionString) as TConnection;
            selectConnection.Open();

            var command = Activator.CreateInstance(typeof(TCommand), cmdText, selectConnection) as TCommand;

            if (values != null)
                command.Parameters.AddRange(values);

            DbDataReader dataReader = command.ExecuteReader();
            if (dataReader.Read())
                dataReaderAction(dataReader);

            dataReader.Close();
            command.Dispose();
            selectConnection.Close();
            selectConnection.Dispose();
        }

        protected async Task ReadDataSingleAsync(Action<DbDataReader> dataReaderAction, string cmdText, params TParameter[] values)
        {
            var selectConnection = Activator.CreateInstance(typeof(TConnection), this.connectionString) as TConnection;
            selectConnection.Open();

            var command = Activator.CreateInstance(typeof(TCommand), cmdText, selectConnection) as TCommand;

            if (values != null)
                command.Parameters.AddRange(values);

            DbDataReader dataReader = await command.ExecuteReaderAsync();
            if (await dataReader.ReadAsync())
                dataReaderAction(dataReader);

            dataReader.Close();
            command.Dispose();
            selectConnection.Close();
            selectConnection.Dispose();
        }

        protected virtual object ExecuteScalar(string cmdText, params TParameter[] values)
        {
            var selectConnection = Activator.CreateInstance(typeof(TConnection), this.connectionString) as TConnection;
            selectConnection.Open();

            var command = Activator.CreateInstance(typeof(TCommand), cmdText, selectConnection) as TCommand;

            if (values != null)
                command.Parameters.AddRange(values);

            object result = command.ExecuteScalar();

            command.Dispose();
            selectConnection.Close();
            selectConnection.Dispose();

            return result;
        }

        protected async Task<object> ExecuteScalarAsync(string cmdText, params TParameter[] values)
        {
            var selectConnection = Activator.CreateInstance(typeof(TConnection), this.connectionString) as TConnection;
            selectConnection.Open();

            var command = Activator.CreateInstance(typeof(TCommand), cmdText, selectConnection) as TCommand;

            if (values != null)
                command.Parameters.AddRange(values);

            object result = await command.ExecuteScalarAsync();

            command.Dispose();
            selectConnection.Close();
            selectConnection.Dispose();

            return result;
        }

        protected int ExecuteNonQuery(string cmdText, params TParameter[] values)
        {
            var selectConnection = Activator.CreateInstance(typeof(TConnection), this.connectionString) as TConnection;
            selectConnection.Open();

            var command = Activator.CreateInstance(typeof(TCommand), cmdText, selectConnection) as TCommand;

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

        protected string DbToEnumDescription<TEnum>(object value) where TEnum : struct
        {
            if (value == DBNull.Value)
                return null;
            else
                return value.ToString().GetEnumDescription<TEnum>();
        }

        protected string GetDayOfWeekName(DateTime dateTime)
        {
            var provider = new System.Globalization.CultureInfo(name: "zh-TW");
            System.Globalization.DateTimeFormatInfo info = System.Globalization.DateTimeFormatInfo.GetInstance(provider);
            string fullName = info.DayNames[(byte)dateTime.DayOfWeek];

            return fullName.Substring(startIndex: 2, length: 1);
        }
    }
}
