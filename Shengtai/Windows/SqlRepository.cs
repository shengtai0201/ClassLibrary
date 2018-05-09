using System.Data.Entity;
using System.Data.SqlClient;

namespace Shengtai.Windows
{
    public abstract class SqlRepository<TContext> : Repository<SqlConnection, SqlCommand, SqlParameter, TContext>
        where TContext : DbContext
    {
        protected SqlRepository(TContext context) : base(context)
        {
        }
    }
}