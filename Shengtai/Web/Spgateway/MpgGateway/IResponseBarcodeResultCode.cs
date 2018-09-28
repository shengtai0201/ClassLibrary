using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway.MpgGateway
{
    /// <summary>
    /// 超商條碼繳費回傳參數 
    /// </summary>
    /// <seealso cref="Shengtai.Web.Spgateway.IResponseResultCode" />
    public interface IResponseBarcodeResultCode : IResponseResultCode
    {
        /// <summary>
        /// 第一段條碼 
        /// </summary>
        /// <value>
        /// 1.若取號成功，此欄位呈現數值。 
        /// 2.若取號失敗，此欄位呈現空值。 
        /// </value>
        [StringLength(20)]
        string Barcode_1 { get; set; }

        /// <summary>
        /// 第二段條碼 
        /// </summary>
        /// <value>
        /// 1.若取號成功，此欄位呈現數值。 
        /// 2.若取號失敗，此欄位呈現空值。 
        /// </value>
        [StringLength(20)]
        string Barcode_2 { get; set; }

        /// <summary>
        /// 第三段條碼 
        /// </summary>
        /// <value>
        /// 1.若取號成功，此欄位呈現數值。 
        /// 2.若取號失敗，此欄位呈現空值。 
        /// </value>
        [StringLength(20)]
        string Barcode_3 { get; set; }
    }
}
