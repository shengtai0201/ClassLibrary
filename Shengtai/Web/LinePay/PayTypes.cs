using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.LinePay
{
    public enum PayTypes
    {
        /// <summary>
        /// 單筆付款 (預設)
        /// </summary>
        NORMAL,

        /// <summary>
        /// 自動付款
        /// </summary>
        PREAPPROVED
    }
}
