using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway.MpgGateway
{
    /// <summary>
    /// Pay2go 電子錢包回傳參數 
    /// </summary>
    /// <seealso cref="Shengtai.Web.Spgateway.ResponseResult" />
    public interface IResponseP2gResult : IResponseResult
    {
        /// <summary>
        /// P2G 交易序號 
        /// </summary>
        /// <value>
        /// P2G 在此筆交易所產生的序號。 
        /// </value>
        [StringLength(25)]
        string P2GTradeNo { get; set; }

        /// <summary>
        /// P2G 支付方式 
        /// </summary>
        /// <value>
        /// 可參考 附件一。 但前面會為 P2G_ 開頭，說明如下： 
        ///     P2G 信用卡交易 P2GPaymentType = P2G_CREDIT 
        ///     P2G WEBATM 交易 P2GPaymentType = P2G_WEBATM 
        ///     P2G ATM 轉帳交易 P2GPaymentType = P2G_VACC 
        ///     P2G 超商代碼繳費交易 P2GPaymentType = P2G_CVS 
        ///     P2G 電子帳戶交易 P2GPaymentType = P2GEACC 
        /// </value>
        [StringLength(10)]
        P2GPaymentTypes P2GPaymentType { get; set; }

        /// <summary>
        /// P2G 交易金額 
        /// </summary>
        /// <value>
        /// 1.純數字不含符號，例：1000。 
        /// 2.幣別：新台幣。 
        /// </value>
        [StringLength(10)]
        int P2GAmt { get; set; }
    }
}
