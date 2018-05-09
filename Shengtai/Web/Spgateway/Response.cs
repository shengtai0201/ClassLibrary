using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway
{
    public class Response
    {
        /// <summary>
        /// 回傳狀態 
        /// </summary>
        /// <value>
        /// 1.若交易付款成功，則回傳 SUCCESS。 
        /// 2.若交易付款失敗，則回傳錯誤代碼。 錯誤代碼請參考十、錯誤代碼。 
        /// </value>
        [StringLength(10)]
        public string Status { get; set; }

        /// <summary>
        /// 回傳訊息 
        /// </summary>
        /// <value>
        /// 商店代號。 
        /// </value>
        [StringLength(20)]
        public string MerchantID { get; set; }

        /// <summary>
        /// 交易資料
        /// AES 加密
        /// </summary>
        /// <value>
        /// 1.將交易資料參數（下方列表中參數）透過商店 Key 及 IV 進行 AES 加密。 
        /// 2.範例請參考八、交易資料 AES 加解密 
        /// </value>
        public string TradeInfo { get; set; }

        /// <summary>
        /// 交易資料
        /// SHA256 加密
        /// </summary>
        /// <value>
        /// 1.將交易資料經過上述 AES 加密過的字串，透過商店 Key 及 IV 再進行 SHA256 加密。 
        /// 2.範例請參考九、交易資料 SHA256 加密 
        /// </value>
        public string TradeSha { get; set; }

        /// <summary>
        /// 串接程式版本
        /// </summary>
        /// <value>
        /// 串接程式版本 
        /// </value>
        [StringLength(5)]
        public string Version { get; set; }
    }
}
