using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai
{
    public abstract class SqlRepository<TContext> : Repository<SqlConnection, SqlCommand, SqlParameter, TContext>
        where TContext : DbContext
    {
        //protected override object ExecuteScalar(string cmdText, params SqlParameter[] values)
        //{
        //    var selectConnection = new SqlConnection(this.connectionString);
        //    selectConnection.Open();

        //    var command = new SqlCommand(cmdText, selectConnection);

        //    if (values != null)
        //    {
        //        foreach (var parameter in values)
        //            command.Parameters.AddWithValue(parameter.ParameterName, parameter.Value);
        //    }
        //    //command.Parameters.AddRange(values);

        //    object result = command.ExecuteScalar();

        //    command.Dispose();
        //    selectConnection.Close();
        //    selectConnection.Dispose();

        //    return result;

        //}
    }
}
