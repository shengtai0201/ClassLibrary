using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway
{
    /// <summary>
    /// 超商代碼繳費回傳參數 
    /// </summary>
    /// <seealso cref="Shengtai.Web.Spgateway.ResponseResult" />
    public interface IResponseCvsResult : IResponseResult
    {
        /// <summary>
        /// 繳費代碼 
        /// </summary>
        /// <value>
        /// 繳費代碼。 
        /// </value>
        [StringLength(30)]
        string CodeNo { get; set; }

        /// <summary>
        /// 繳費代碼 
        /// </summary>
        /// <value>
        /// 1=7-11 統一超商 
        /// 2=全家便利商店 
        /// 3=OK 便利商店 
        /// 4=萊爾富便利商店 
        /// </value>
        [StringLength(1)]
        object StoreType { get; set; }

        /// <summary>
        /// 繳費門市代號  
        /// </summary>
        /// <value>
        /// 繳費門市代號 (全家回傳門市中文名稱) 
        /// </value>
        [StringLength(10)]
        string StoreID { get; set; }
    }
}
