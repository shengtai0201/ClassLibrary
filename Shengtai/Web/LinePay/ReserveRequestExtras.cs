using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.LinePay
{
    public class ReserveRequestAddFriend
    {
        /// <summary>
        /// 服務類型
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        ///  ID 清單(在商家中心完成註冊的 LINE@/LINE 官方帳號清單)
        /// </summary>
        [JsonProperty("idList")]
        public ICollection<string> IdList { get; set; }
    }

    public class ReserveRequestExtras
    {
        /// <summary>
        /// 加好友清單
        /// </summary>
        [JsonProperty("addFriends")]
        public ICollection<ReserveRequestAddFriend> AddFriends { get; set; }

        /// <summary>
        /// 需要付款的商店/分店名稱(僅會顯示前 100 字元)
        /// </summary>
        [StringLength(200)]
        [JsonProperty("branchName")]
        public string BranchName { get; set; }
    }
}
