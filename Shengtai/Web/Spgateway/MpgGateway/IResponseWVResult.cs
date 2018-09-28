using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway.MpgGateway
{
    /// <summary>
    /// WEBATM、ATM繳費回傳參數
    /// </summary>
    /// <seealso cref="Shengtai.Web.Spgateway.ResponseResult" />
    public interface IResponseWVResult : IResponseResult
    {
        /// <summary>
        /// 付款人金融機構代碼 
        /// </summary>
        /// <value>
        /// 由代收款金融機構所回應的付款人金融機構代碼。 
        /// </value>
        [StringLength(10)]
        string PayBankCode { get; set; }

        /// <summary>
        /// 付款人金融機構帳號末五碼 
        /// </summary>
        /// <value>
        /// 由代收款金融機構所回應的付款人金融機構帳號末五碼。
        /// </value>
        [StringLength(5)]
        int PayerAccount5Code { get; set; }
    }
}
