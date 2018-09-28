using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway.QueryTradeInfo
{
    /// <summary>
    /// 超商代碼 、超商條碼、ATM轉帳 專屬欄位
    /// </summary>
    public interface IResponseCBVResult : IResponseResult
    {
        /// <summary>
        /// 付款資訊
        /// </summary>
        /// <value>
        /// 1.付款方式為超商代碼(CVS)時，此欄位為超商繳款代碼。 
        /// 2.付款方式為條碼(BARCODE)時，此欄位為繳款條碼。此欄位會將三段條碼資訊用逗號”,”組合後回傳。
        /// 3.付款方式為ATM轉帳時，此欄位為金融機構的轉帳帳號，括號內為金融機構代碼，例：(031)1234567890。
        /// </value>
        [StringLength(50)]
        string PayInfo { get; set; }

        /// <summary>
        /// 繳費有效期限
        /// </summary>
        /// <value>
        /// 1.格式為 Y-m-d H:i:s 。
        /// 例：2014-06-29 23:59:59。
        /// </value>
        DateTime ExpireDate { get; set; }
    }
}
