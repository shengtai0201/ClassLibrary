using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai
{
    public abstract class NpgsqlRepository<TContext> : Repository<NpgsqlConnection, NpgsqlCommand, NpgsqlParameter, TContext>
        where TContext : DbContext
    {
        //public NpgsqlRepository(string connectionStringName) : base(connectionStringName) { }
    }
}
