using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Shengtai.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Shengtai
{
    public abstract class SqlRepository<TContext, TDefaultConnection> : Repository<SqlConnection, SqlCommand, SqlParameter, SqlDataAdapter, TContext, TDefaultConnection>
        where TContext : DbContext
        where TDefaultConnection : IDefaultConnection
    {
        protected SqlRepository(IAppSettings<TDefaultConnection> appSettings, TContext dbContext) : base(appSettings, dbContext) { }
    }
}
