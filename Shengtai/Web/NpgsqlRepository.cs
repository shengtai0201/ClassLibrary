using Npgsql;
using System.Data.Entity;

namespace Shengtai.Web
{
    public abstract class NpgsqlRepository<TContext> : Repository<NpgsqlConnection, NpgsqlCommand, NpgsqlParameter, TContext>
        where TContext : DbContext
    {
        protected NpgsqlRepository(TContext context = null) : base(context)
        {
        }
    }
}