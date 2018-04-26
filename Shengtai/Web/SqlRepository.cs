using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web
{
    public abstract class SqlRepository<TContext> : Repository<SqlConnection, SqlCommand, SqlParameter, TContext>
        where TContext : DbContext
    {
        protected SqlRepository(TContext context = null) : base(context) { }
    }
}
