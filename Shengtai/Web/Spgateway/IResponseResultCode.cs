using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway
{
    /// <summary>
    /// ATM 轉帳、超商代碼繳費、超商條碼繳費、超商取貨付款共同回傳參數 
    /// </summary>
    public interface IResponseResultCode
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
        /// 支付金額 
        /// </summary>
        /// <value>
        /// 本次交易金額，例：1000。 
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
        /// 1.商店自訂訂單編號，限英、數字、”_ ”格式。 例：201406010001。 
        /// 2.同一商店中此編號不可重覆。 
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
        /// 繳費截止日期 
        /// </summary>
        /// <value>
        /// 回傳格式為 yyyy-mm-dd。 
        /// </value>
        DateTime ExpireDate { get; set; }
    }
}
