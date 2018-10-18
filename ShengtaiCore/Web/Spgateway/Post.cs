using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway
{
    public abstract class Post
    {
        /// <summary>
        /// 商店代號
        /// </summary>
        /// <value>
        /// 智付通商店代號。
        /// </value>
        [StringLength(15)]
        [Required]
        [Column(Order = 0)]
        public string MerchantID { get; set; }

        /// <summary>
        /// 串接程式版本 
        /// </summary>
        /// <value>
        /// 請帶 1.4。 
        /// </value>
        [StringLength(5)]
        [Required]
        [Column(Order = 3)]
        public string Version { get; set; }

        /// <summary>
        /// 回傳格式
        /// </summary>
        /// <value>
        /// JSON 或是 String。
        /// </value>
        [StringLength(6)]
        [Required]
        [Column(Order = 1)]
        public string RespondType { get; set; }

        /// <summary>
        /// 時間戳記 
        /// </summary>
        /// <value>
        /// 自從 Unix 纪元（格林威治時間 1970 年 1 月 1 日 00:00:00）到當前時間的秒數，若 以 php 程式語言為例，即為呼叫 time()函式 所回傳的值。
        /// 例：2014-05-15 15:00:00(+08:00 時區)這個時間的時間戳記為 1400137200。 
        /// </value>
        [StringLength(50)]
        [Required]
        [Column(Order = 2)]
        public string TimeStamp { get; set; }

        /// <summary>
        /// 商店訂單編號 
        /// </summary>
        /// <value>
        /// 1.商店自訂訂單編號，限英、數字、”_ ”格式。 例：201406010001。 
        /// 2.長度限制為 20 字。 
        /// 3.同一商店中此編號不可重覆。 
        /// </value>
        [StringLength(20)]
        [Required]
        [Column(Order = 4)]
        public string MerchantOrderNo { get; set; }

        /// <summary>
        /// 訂單金額 
        /// </summary>
        /// <value>
        /// 1.純數字不含符號，例：1000。 
        /// 2.幣別：新台幣。 
        /// </value>
        [StringLength(10)]
        [Required]
        [Column(Order = 5)]
        public int Amt { get; set; }
    }
}
