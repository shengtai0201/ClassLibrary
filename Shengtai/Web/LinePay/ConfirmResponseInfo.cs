using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.LinePay
{
    public class ConfirmResponsePayInfo
    {
        /// <summary>
        /// 使用的付款方式 (信用卡：CREDIT_CARD，餘額：BALANCE，折扣: DISCOUNT)
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; set; }

        /// <summary>
        /// 付款金額
        /// </summary>
        [JsonProperty("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// (paytype = Preapproved 之時候) 信用卡暱稱
        /// 由 LINE Pay 管理的最初登錄的信用卡名稱。
        /// 如果 LINE Pay 用戶未設定暱稱，則顯示空白文字 ("")。
        /// 此暱稱可由用戶在 LINE Pay 中請求更改，更改後的名稱不會告知商家。
        /// </summary>
        [JsonProperty("creditCardNickname")]
        public string CreditCardNickname { get; set; }

        /// <summary>
        /// (paytype = Preapproved 之時候) 信用卡品牌
        /// </summary>
        [JsonProperty("creditCardBrand")]
        public string CreditCardBrand { get; set; }
    }

    public class ConfirmResponseInfo
    {
        /// <summary>
        /// 商家在付款 reserve 時傳送的訂單編號
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// 付款 reserve 後，做為結果所收到的交易編號
        /// </summary>
        [JsonProperty("transactionId")]
        public int TransactionId { get; set; }

        /// <summary>
        /// 可選擇的授權過期日 (ISO 8601)
        /// 當付款狀態為 AUTHORIZATION (capture=false) 時
        /// </summary>
        [JsonProperty("authorizationExpireDate")]
        public string AuthorizationExpireDate { get; set; }

        [JsonProperty("payInfo")]
        public ICollection<ConfirmResponsePayInfo> PayInfo { get; set; }

        /// <summary>
        /// paytype 為 Preapproved 之時候，以後可直接選用的自動付款金鑰 (15位數)
        /// </summary>
        [JsonProperty("regKey")]
        public string RegKey { get; set; }
    }
}
