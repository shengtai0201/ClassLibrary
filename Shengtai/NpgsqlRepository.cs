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
        protected NpgsqlRepository(bool setService) : base(null, setService) { }
        protected NpgsqlRepository(TContext context, bool setService = false) : base(context, setService) { }
    }
}
