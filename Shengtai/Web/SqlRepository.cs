using System.Data.Entity;
using System.Data.SqlClient;

namespace Shengtai.Web
{
    public abstract class SqlRepository<TContext> : Repository<SqlConnection, SqlCommand, SqlParameter, TContext>
        where TContext : DbContext
    {
        protected SqlRepository(TContext context = null) : base(context)
        {
        }
    }
}