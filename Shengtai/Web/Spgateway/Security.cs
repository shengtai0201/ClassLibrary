using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway
{
    public abstract class Security
    {
        protected readonly string key;
        protected readonly string iv;

        public Security(string key, string iv)
        {
            this.key = key;
            this.iv = iv;
        }

        protected int? GetOrder(PropertyInfo info)
        {
            foreach (var attribute in info.GetCustomAttributes(true))
            {
                if (attribute is ColumnAttribute column)
                    return column.Order;
            }

            return null;
        }

        public abstract string GetTradeInfo(object trade);
        public abstract string GetTradeSha(string tradeInfo);
    }
}
