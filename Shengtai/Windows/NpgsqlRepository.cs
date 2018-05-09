using Npgsql;
using System.Data.Entity;

namespace Shengtai.Windows
{
    public abstract class NpgsqlRepository<TContext> : Repository<NpgsqlConnection, NpgsqlCommand, NpgsqlParameter, TContext>
        where TContext : DbContext
    {
        protected NpgsqlRepository(TContext context) : base(context)
        {
        }
    }
}