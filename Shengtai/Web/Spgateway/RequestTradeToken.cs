using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway
{
    /// <summary>
    /// 信用卡快速結帳參數
    /// </summary>
    /// <seealso cref="Shengtai.Web.Spgateway.RequestTrade" />
    public class RequestTradeToken : RequestTrade
    {
        /// <summary>
        /// 付款人綁定資料 
        /// </summary>
        /// <value>
        /// 1.可對應付款人之資料，用於綁定付款人與 信用卡卡號時使用，例：會員編號、 Email。 
        /// 2.限英、數字，「.」、「_」、「@」、「-」格 式。
        /// </value>
        [StringLength(20)]
        [Required]
        public string TokenTerm { get; set; }

        /// <summary>
        /// 指定付款人信用 卡快速結帳必填 欄位 
        /// </summary>
        /// <value>
        /// 可指定付款人需填寫的信用卡資訊，不同的 參數值對應填寫不同的資訊，參數值與對應 資訊說明如下：   
        ///     1 = 必填信用卡到期日與背面末三碼   
        ///     2 = 必填信用卡到期日   
        ///     3 = 必填背面末三碼 未有此參數或帶入其他無效參數，系統預設 為參數 1。 
        /// </value>
        [StringLength(1)]
        public int? TokenTermDemand { get; set; } = 1;

        public RequestTradeToken() : base()
        {
            this.TokenTermDemand = 1;
        }
    }
}
