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
    /// <seealso cref="Shengtai.Web.Spgateway.ResponseResult" />
    public interface IResponseBarcodeResult : IResponseResult
    {
        /// <summary>
        /// 第一段條碼 
        /// </summary>
        /// <value>
        /// 繳費條碼第一段條碼資料。 
        /// </value>
        [StringLength(20)]
        string Barcode_1 { get; set; }

        /// <summary>
        /// 第二段條碼 
        /// </summary>
        /// <value>
        /// 繳費條碼第二段條碼資料。 
        /// </value>
        [StringLength(20)]
        string Barcode_2 { get; set; }

        /// <summary>
        /// 第三段條碼 
        /// </summary>
        /// <value>
        /// 繳費條碼第三段條碼資料。 
        /// </value>
        [StringLength(20)]
        string Barcode_3 { get; set; }

        /// <summary>
        /// 繳費超商 
        /// </summary>
        /// <value>
        /// 付款人至超商繳費，該收款超商的代碼， 
        /// SEVEN：7-11 
        /// FAMILY：全家 
        /// OK：OK 超商 
        /// HILIFE：萊爾富 
        /// </value>
        [StringLength(8)]
        string PayStore { get; set; }
    }
}
