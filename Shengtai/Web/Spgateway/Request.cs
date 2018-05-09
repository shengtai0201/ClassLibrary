using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway
{
    public class Request
    {
        /// <summary>
        /// 商店代號
        /// </summary>
        /// <value>
        /// 智付通商店代號。
        /// </value>
        [StringLength(15)]
        [Required]
        public string MerchantID { get; set; }

        /// <summary>
        /// 交易資料
        /// AES 加密
        /// </summary>
        /// <value>
        /// 將交易資料參數（下方列表中參數）透過 商店 Key 及 IV 進行 AES 加密。
        /// </value>
        [Required]
        public string TradeInfo { get; set; }

        /// <summary>
        /// 交易資料
        /// SHA256 加密
        /// </summary>
        /// <value>
        /// 將交易資料經過上述 AES 加密過的字串， 透過商店 Key 及 IV 進行 SHA256 加密。
        /// </value>
        [Required]
        public string TradeSha { get; set; }

        /// <summary>
        /// 串接程式版本
        /// </summary>
        /// <value>
        /// 請帶 1.4。
        /// </value>
        [StringLength(5)]
        [Required]
        public string Version { get; set; }// = "1.4";

        public Request(RequestTrade trade, Crypto crypto)
        {
            this.MerchantID = trade.MerchantID;
            this.TradeInfo = crypto.GetTradeInfo(trade);
            this.TradeSha = crypto.GetTradeSha(this.TradeInfo);
            this.Version = "1.4";
        }
    }
}
