using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Shengtai
{
    public abstract class SqlRepository<TContext> : Repository<SqlConnection, SqlCommand, SqlParameter, SqlDataAdapter, TContext>
       where TContext : DbContext
    {
        protected SqlRepository(IAppSettings appSettings, TContext dbContext) : base(appSettings, dbContext) { }
    }
}
