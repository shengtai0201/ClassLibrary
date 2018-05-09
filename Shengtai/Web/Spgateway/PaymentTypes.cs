using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway
{
    public enum PaymentTypes
    {
        /// <summary>
        /// 信用卡(即時交易)
        /// </summary>
        CREDIT,

        /// <summary>
        /// WebATM(即時交易)
        /// </summary>
        WEBATM,

        /// <summary>
        /// ATM 轉帳(非即時交易)
        /// </summary>
        VACC,

        /// <summary>
        /// 超商代碼繳費(非即時交易)
        /// </summary>
        CVS,

        /// <summary>
        /// 超商條碼繳費(非即時交易)
        /// </summary>
        BARCODE,

        /// <summary>
        /// 超商取貨付款(非即時交易)
        /// </summary>
        CVSCOM
    }
}
