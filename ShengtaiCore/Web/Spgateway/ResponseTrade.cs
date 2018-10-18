using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway
{
    /// <summary>
    /// 回傳參數
    /// </summary>
    public class ResponseTrade<T>
    {
        /// <summary>
        /// 回傳狀態 
        /// </summary>
        /// <value>
        /// 1.若交易付款成功，則回傳 SUCCESS。 
        /// 2.若交易付款失敗，則回傳錯誤代碼。 錯誤代碼請參考十、錯誤代碼。 
        /// 3.若使用新增自訂支付欄位之交易，則回傳 CUSTOM。 
        /// </value>
        [StringLength(10)]
        public string Status { get; set; }

        /// <summary>
        /// 回傳訊息 
        /// </summary>
        /// <value>
        /// 文字，敘述此次交易狀態。 
        /// </value>
        [StringLength(50)]
        public string Message { get; set; }

        /// <summary>
        /// 回傳參數 
        /// </summary>
        /// <value>
        /// 當 RespondType 為 JSON 時，回傳參數會放至此陣列下。 
        /// </value>
        public T Result { get; set; }
    }
}
