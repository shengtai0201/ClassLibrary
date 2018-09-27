using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway
{
    /// <summary>
    /// 所有支付方式共同回傳參數 
    /// </summary>
    public interface IResponseResult
    {
        /// <summary>
        /// 商店代號
        /// </summary>
        /// <value>
        /// 智付通商店代號。
        /// </value>
        [StringLength(15)]
        string MerchantID { get; set; }

        /// <summary>
        /// 交易金額 
        /// </summary>
        /// <value>
        /// 1.純數字不含符號，例：1000。 
        /// 2.幣別：新台幣。 
        /// </value>
        [StringLength(10)]
        int Amt { get; set; }

        /// <summary>
        /// 智付通交易序號 
        /// </summary>
        /// <value>
        /// 智付通在此筆交易取號成功時所產生的序號。 
        /// </value>
        [StringLength(20)]
        string TradeNo { get; set; }

        /// <summary>
        /// 商店訂單編號  
        /// </summary>
        /// <value>
        /// 商店自訂訂單編號。 
        /// </value>
        [StringLength(20)]
        string MerchantOrderNo { get; set; }

        /// <summary>
        /// 支付方式 
        /// </summary>
        /// <value>
        /// 請參考 附件一。 
        /// </value>
        [StringLength(10)]
        PaymentTypes PaymentType { get; set; }

        /// <summary>
        /// 回傳格式 
        /// </summary>
        /// <value>
        /// JSON 格式。 
        /// </value>
        [StringLength(10)]
        string RespondType { get; set; }

        /// <summary>
        /// 支付完成時間 
        /// </summary>
        /// <value>
        /// 回傳格式為：2014-06-2516:43:49 
        /// </value>
        DateTime PayTime { get; set; }

        /// <summary>
        /// 交易 IP 
        /// </summary>
        /// <value>
        /// 付款人取號或交易時的 IP。 
        /// </value>
        [StringLength(15)]
        string IP { get; set; }

        /// <summary>
        /// 款項保管銀行 
        /// </summary>
        /// <value>
        /// 1.該筆交易履約保證銀行。 
        /// 2.如商店是直接與銀行簽約的信用卡特約商店，當使用信用卡支付時，本欄位的值會以空值回傳。 
        /// 3.履保銀行英文代碼與中文名稱對應如下： 
        ///     ［Esun］：玉山銀行 
        ///     ［Taishin］：台新銀行 
        ///     ［HNCB］： 華南銀行 
        /// </value>
        [StringLength(10)]
        string EscrowBank { get; set; }
    }
}
