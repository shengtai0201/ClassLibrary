using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway.QueryTradeInfo
{
    public class RequestTrade : Post
    {
        /// <summary>
        /// 檢查碼
        /// </summary>
        /// <value>
        /// 請參考 附件一 說明。
        /// </value>
        [StringLength(255)]
        [Required]
        public string CheckValue { get; set; }
    }
}
