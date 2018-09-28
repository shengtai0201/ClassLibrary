using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway.MpgGateway
{
    /// <summary>
    /// 超商物流回傳參數 
    /// </summary>
    /// <seealso cref="Shengtai.Web.Spgateway.ResponseResult" />
    public interface IResponseCvscomResult : IResponseResult
    {
        /// <summary>
        /// 超商門市編號 
        /// </summary>
        /// <value>
        /// 取貨門市編號 
        /// </value>
        [StringLength(10)]
        string StoreCode { get; set; }

        /// <summary>
        /// 超商門市名稱  
        /// </summary>
        /// <value>
        /// 取貨門市中文名稱 
        /// </value>
        [StringLength(15)]
        string StoreName { get; set; }

        /// <summary>
        /// 超商類別名稱  
        /// </summary>
        /// <value>
        /// 回傳[全家] 、[OK] 、[萊爾富] 
        /// </value>
        [StringLength(10)]
        object StoreType { get; set; }

        /// <summary>
        /// 超商門市地址  
        /// </summary>
        /// <value>
        /// 取貨門市地址 
        /// </value>
        [StringLength(100)]
        string StoreAddr { get; set; }

        /// <summary>
        /// 取件交易方式  
        /// </summary>
        /// <value>
        /// 1 = 取貨付款 
        /// 3 = 取貨不付款 
        /// </value>
        [StringLength(1)]
        int TradeType { get; set; }

        /// <summary>
        /// 取貨人  
        /// </summary>
        /// <value>
        /// 取貨人姓名 
        /// </value>
        [StringLength(20)]
        string CVSCOMName { get; set; }

        /// <summary>
        /// 取貨人手機號碼  
        /// </summary>
        /// <value>
        /// 取貨人手機號碼  
        /// </value>
        [StringLength(10)]
        string CVSCOMPhone { get; set; }
    }
}
