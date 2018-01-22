using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Shengtai
{
    public abstract class SqlRepository<TContext> : Repository<SqlConnection, SqlCommand, SqlParameter, TContext>
       where TContext : DbContext
    {
        protected SqlRepository(TContext dbContext) : base(dbContext)
        {
        }
    }
}
