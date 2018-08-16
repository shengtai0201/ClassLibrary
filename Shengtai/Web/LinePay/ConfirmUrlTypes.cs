using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.LinePay
{
    public enum ConfirmUrlTypes
    {
        /// <summary>
        /// 手機交易流程 (預設)
        /// </summary>
        CLIENT,

        /// <summary>
        /// 網站交易流程。用戶只需要查看
        /// LINE Pay 的付款資訊畫面，然後通知商家
        /// 伺服器可以付款。
        /// </summary>
        SERVER
    }
}
