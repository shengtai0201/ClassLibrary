using Microsoft.EntityFrameworkCore;
using Npgsql;
using Shengtai.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shengtai
{
    public abstract class NpgsqlRepository<TContext, TDefaultConnection> : Repository<NpgsqlConnection, NpgsqlCommand, NpgsqlParameter, NpgsqlDataAdapter, TContext, TDefaultConnection>
        where TContext : DbContext
        where TDefaultConnection : IDefaultConnection
    {
        protected NpgsqlRepository(IAppSettings<TDefaultConnection> appSettings, TContext dbContext) : base(appSettings, dbContext) { }
    }
}
