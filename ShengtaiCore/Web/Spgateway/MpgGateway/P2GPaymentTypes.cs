using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway.MpgGateway
{
    public enum P2GPaymentTypes
    {
        /// <summary>
        /// 信用卡(即時交易)
        /// </summary>
        P2G_CREDIT,

        /// <summary>
        /// WebATM(即時交易)
        /// </summary>
        P2G_WEBATM,

        /// <summary>
        /// ATM 轉帳(非即時交易)
        /// </summary>
        P2G_VACC,

        /// <summary>
        /// 超商代碼繳費(非即時交易)
        /// </summary>
        P2G_CVS,

        /// <summary>
        /// 電子帳戶交易 
        /// </summary>
        P2GEACC
    }
}
