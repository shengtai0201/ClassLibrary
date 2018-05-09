using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway
{
    /// <summary>
    /// ATM 轉帳回傳參數 
    /// </summary>
    /// <seealso cref="Shengtai.Web.Spgateway.IResponseResultCode" />
    public interface IResponseVaccResultCode : IResponseResultCode
    {
        /// <summary>
        /// 金融機構代碼 
        /// </summary>
        /// <value>
        /// 1.若取號成功，此欄位呈現數值。 
        /// 2.若取號失敗，此欄位呈現空值。 
        /// </value>
        [StringLength(10)]
        string BankCode { get; set; }

        /// <summary>
        /// 繳費代碼 
        /// </summary>
        /// <value>
        /// 1.若取號成功，此欄位呈現數值。 
        /// 2.若取號失敗，此欄位呈現空值。 
        /// </value>
        [StringLength(30)]
        string CodeNo { get; set; }
    }
}
