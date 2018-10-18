using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway
{
    public enum PaymentMethods
    {
        /// <summary>
        /// 台灣發卡機構核發之信用卡
        /// </summary>
        CREDIT,

        /// <summary>
        /// 國外發卡機構核發之卡
        /// </summary>
        FOREIGN,

        /// <summary>
        /// 銀聯卡
        /// </summary>
        UNIONPAY,

        /// <summary>
        /// GooglePay
        /// </summary>
        GOOGLEPAY,

        /// <summary>
        /// SamsungPay
        /// </summary>
        SAMSUNGPAY
    }
}
