using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.LinePay
{
    public class ReserveResponsePaymentUrl
    {
        /// <summary>
        /// 付款請求後所前往的網頁 URL
        /// 在網頁交易環境中請求付款時使用
        /// LINE Pay 等待付款畫面的 URL
        /// 重新導向到提供的 URL，不附帶任何額外參數
        /// 在桌機打開跳窗時， 其大小為：Width: 700px, Height : 546px
        /// </summary>
        [JsonProperty("web")]
        public string Web { get; set; }

        /// <summary>
        /// 前往付款畫面的應用程式 URL
        /// 在機交易請求付款時使用
        /// 將 URL 從商家手機交易重新導向到 LINE 應用程式
        /// </summary>
        [JsonProperty("app")]
        public string App { get; set; }
    }

    public class ReserveResponseInfo
    {
        /// <summary>
        /// 交易編號 (19 位數)
        /// </summary>
        [JsonProperty("transactionId")]
        public int TransactionId { get; set; }

        [JsonProperty("paymentUrl")]
        public ReserveResponsePaymentUrl PaymentUrl { get; set; }

        /// <summary>
        /// 在 LINE Pay app 輸入代碼來代替適用掃描器(Scanner)之時候所適用的代碼值(共 12 位數)。
        /// (LINE Pay app 的掃描器是從 5.1 版本開始支援)
        /// </summary>
        [JsonProperty("paymentAccessToken")]
        public string PaymentAccessToken { get; set; }
    }
}
