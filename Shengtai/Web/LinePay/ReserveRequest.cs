using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.LinePay
{
    // 付款
    public class ReserveRequest : Request<ReserveResponseInfo>
    {
        /// <summary>
        /// 產品名稱 (charset:"UTF-8")
        /// </summary>
        [Required]
        [StringLength(4000)]
        [JsonProperty("productName")]
        public string ProductName { get; set; }

        /// <summary>
        /// 產品影像 URL（或使用品牌 logo URL）顯示於付款畫面上的影像 URL 
        /// </summary>
        [StringLength(500)]
        [JsonProperty("productImageUrl ")]
        public string ProductImageUrl { get; set; }

        /// <summary>
        /// LINE 用戶 ID
        /// 將要進行付款的 LINE 使用者之 mid
        /// </summary>
        [StringLength(50)]
        [JsonProperty("mid")]
        public string Mid { get; set; }

        /// <summary>
        /// oneTimeKey 是讀取 LINE Pay app 所提供之二維碼、條碼後之結果。替代 LINE Pay 會員之 mid。有效時間為 5 分鐘，與 rserve 同時會被刪除。
        /// LINE Pay app 的 QR/BarCode 是從 5.1 版本開始支援
        /// </summary>
        [StringLength(12)]
        [JsonProperty("oneTimeKey")]
        public string OneTimeKey { get; set; }

        /// <summary>
        /// 買家在 LINE Pay 選擇付款方式並輸入密碼後，被重新導向到商家的 URL。
        /// 在重新導向的 URL 上，商家可以呼叫付款 confirm API 並完成付款。
        /// LINE Pay 會傳遞額外的 "transactionId" 參數。
        /// </summary>
        [Required]
        [StringLength(500)]
        [JsonProperty("confirmUrl")]
        public string ConfirmUrl { get; set; }

        /// <summary>
        /// confirmUrl 類型。買家在 LINE Pay 選擇付款方式並輸入密碼後，被重新導向到的 URL 所屬的類型。
        /// </summary>
        [JsonProperty("confirmUrlType")]
        public string ConfirmUrlType { get; set; } = ConfirmUrlTypes.CLIENT.ToString();

        public void SetConfirmUrlType(ConfirmUrlTypes confirmUrlType)
        {
            this.ConfirmUrlType = confirmUrlType.ToString();
        }

        /// <summary>
        /// User 前往 confirmUrl 移動時，確認使用的瀏覽器相同與否
        /// true: 若買家請求付款的瀏覽器與實際打開 confirmUrl 的瀏覽器不同之時，LINE Pay 將會提供請買家回到原本的瀏覽器的介紹頁面。
        /// false(預設): 不會確認瀏覽器，立即打開 confirmUrl。
        /// </summary>
        [JsonProperty("checkConfirmUrlBrowser")]
        public bool CheckConfirmUrlBrowser { get; set; } = false;

        /// <summary>
        /// 取消付款頁面的 URL
        /// 當 LINE Pay 用戶取消付款後，從 LINE 應用程式付款畫面重新導向的 URL(取消付款後，透過行動裝置進入商家應用程式或網站的商家 URL)。
        /// 商家傳送的 URL 會依現況直接使用。
        /// LINE Pay 不會傳遞任何額外的參數。
        /// </summary>
        [StringLength(500)]
        [JsonProperty("cancelUrl")]
        public string CancelUrl { get; set; }

        /// <summary>
        /// 在 Android 各應用程式間轉換時，防止網路釣魚詐騙的資訊。
        /// </summary>
        [StringLength(4000)]
        [JsonProperty("packageName")]
        public string PackageName { get; set; }

        /// <summary>
        /// 商家與該筆付款請求對應的訂單編號
        /// 這是商家自行管理的唯一編號。
        /// </summary>
        [Required]
        [StringLength(100)]
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// 收件人的聯絡資訊 (用於風險管理)
        /// </summary>
        [StringLength(100)]
        [JsonProperty("deliveryPlacePhone")]
        public string DeliveryPlacePhone { get; set; }

        /// <summary>
        /// 付款類型
        /// </summary>
        [StringLength(12)]
        [JsonProperty("payType")]
        public string PayType { get; set; } = PayTypes.NORMAL.ToString();

        public void SetPayType(PayTypes payType)
        {
            this.PayType = payType.ToString();
        }

        /// <summary>
        /// 等待付款畫面 (paymentUrl) 的語言代碼。共支援六種語言。
        /// 語言代碼不是強制性的，如果未收到此代碼，則會根據 accept-language 標頭支援多語言。
        /// 如果收到不支援的 langCd，預設會使用英文 ("en")。
        /// BCP-47 格式：http://en.wikipedia.org/wiki/IETF_language_tag
        /// </summary>
        [JsonProperty("langCd")]
        public string LangCd { get; set; } = "zh-Hant";

        /// <summary>
        /// 指定是否請款
        /// true:呼叫付款 confirm API 時，立即進行付款授權與請款 (預設)。
        /// false:呼叫付款 confirm API 時，只有經過授權，然後透過呼叫 "請款 API" 分開請款，才能完成付款。
        /// </summary>
        [JsonProperty("capture")]
        public bool Capture { get; set; } = true;

        [JsonProperty("extras")]
        public ReserveRequestExtras Extras { get; set; }
    }
}
