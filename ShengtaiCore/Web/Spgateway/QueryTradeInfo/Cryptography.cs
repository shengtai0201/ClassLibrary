using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway.QueryTradeInfo
{
    public class Cryptography : Security
    {
        public Cryptography(string key, string iv) : base(key, iv)
        {
        }

        public override string GetTradeInfo(object trade)
        {
            var properties = trade.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Select(x => new { x.Name, Value = x.GetValue(trade, null) })
                .Where(x => x.Value != null && !string.IsNullOrWhiteSpace(x.Value.ToString()))
                .OrderBy(x => x.Name)
                .ToDictionary(x => x.Name, x => x.Value);

            return string.Join("&", properties.Select(x => x.Key + "=" + x.Value));
        }

        public override string GetTradeSha(string tradeInfo)
        {
            var s = $"IV={this.iv}&{tradeInfo}&Key={this.key}";
            var buffer = Encoding.UTF8.GetBytes(s);

            var sha = new SHA256Managed();
            var hash = sha.ComputeHash(buffer);

            var builder = new StringBuilder();
            foreach (var b in hash)
                builder.AppendFormat("{0:x2}", b);

            return builder.ToString().ToUpper();
        }
    }
}
