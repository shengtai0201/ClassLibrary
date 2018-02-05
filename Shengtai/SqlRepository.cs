using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai
{
    public abstract class SqlRepository<TContext> : Repository<SqlConnection, SqlCommand, SqlParameter, TContext>
        where TContext : DbContext
    {
        protected SqlRepository(bool setService) : base(null, setService) { }
        protected SqlRepository(TContext context, bool setService = false) : base(context, setService) { }
    }
}
