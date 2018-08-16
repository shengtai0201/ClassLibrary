using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.LinePay
{
    public class Request<T>
    {
        /// <summary>
        /// 付款金額
        /// </summary>
        [Required]
        [JsonProperty("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// 付款貨幣 (ISO 4217)
        /// </summary>
        [Required]
        [StringLength(3)]
        [JsonProperty("currency")]
        public string Currency { get; set; }

        public void SetCurrency(Currencies currency)
        {
            this.Currency = currency.ToString();
        }

        public async Task<Response<T>> PostAsync(string channelId, string channelSecret, string requestUri)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-LINE-ChannelId", channelId);
                client.DefaultRequestHeaders.Add("X-LINE-ChannelSecret", channelSecret);

                var content = new StringContent(JsonConvert.SerializeObject(this, Formatting.Indented), Encoding.UTF8, "application/json");

                var response = await client.PostAsync(requestUri, content);
                var value = await response.Content.ReadAsStringAsync();

                return JsonConvert.DeserializeObject<Response<T>>(value);
            }
        }
    }
}
