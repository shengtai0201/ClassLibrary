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

        protected Repository(IConfiguration configuration, TDbContext dbContext)
        {
            this.DbContext = dbContext;

            var builer = new ConfigurationBuilder();
        }

        protected void ExecuteReader()
        {
            string cmdText = @"SELECT u.Id FROM AspNetUsers u WHERE u.InvitationCode = @InvitationCode";
            var connection = new SqlConnection(System.Configuration.GetConnectionString("DefaultConnection"));
            connection.Open();

            var command = new SqlCommand(cmdText, connection);
            command.Parameters.AddWithValue(parameterName: "InvitationCode", value: result);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                id = reader["Id"].ToString();

            reader.Close();
            command.Dispose();
            connection.Close();
            connection.Dispose();
        }
    }
}
