using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.LinePay
{
    public class Response<T>
    {
        /// <summary>
        /// 結果代碼
        /// </summary>
        [StringLength(4)]
        [JsonProperty("returnCode")]
        public string ReturnCode { get; set; }

        public string GetReturnCode()
        {
            switch (this.ReturnCode)
            {
                case "0000":
                    return "成功";
                case "1101":
                    return "買家不是 LINE Pay 會員";
                case "1102":
                    return "買方被停止交易";
                case "1104":
                    return "找不到商家";
                case "1105":
                    return "此商家無法使用 LINE Pay";
                case "1106":
                    return "標頭資訊錯誤";
                case "1110":
                    return "無法使用的信用卡";
                case "1124":
                    return "金額錯誤 (scale)";
                case "1133":
                    return "非有效之 oneTimeKey。";
                case "1141":
                    return "付款帳戶狀態錯誤";
                case "1142":
                    return "餘額不足";
                case "1145":
                    return "正在進行付款";
                case "1150":
                    return "找不到交易";
                case "1152":
                    return "已有保存付款";
                case "1153":
                    return "付款 reserve 時的金額與申請的金額不一致";
                case "1159":
                    return "無付款申請資訊";
                case "1169":
                    return "付款 confirm 所需要資訊錯誤（在 LINE Pay）";
                case "1170":
                    return "使用者帳戶的餘額有變動";
                case "1172":
                    return "已有同一訂單編號的交易資料";
                case "1178":
                    return "不支援的貨幣";
                case "1180":
                    return "付款時限已過";
                case "1194":
                    return "此商家無法使用自動付款";
                case "1198":
                    return "正在處理請求...";
                case "1199":
                    return "內部請求錯誤";
                case "1280":
                    return "信用卡付款時候發生了臨時錯誤";
                case "1281":
                    return "信用卡付款錯誤";
                case "1282":
                    return "信用卡授權錯誤";
                case "1283":
                    return "因異常交易疑慮暫停交易，請洽 LINE Pay 客服確認";
                case "1284":
                    return "暫時無法以信用卡付款";
                case "1285":
                    return "信用卡資訊不完整";
                case "1286":
                    return "信用卡付款資訊不正確";
                case "1287":
                    return "信用卡已過期";
                case "1288":
                    return "信用卡的額度不足";
                case "1289":
                    return "超過信用卡付款金額上限";
                case "1290":
                    return "超過一次性付款的額度";
                case "1291":
                    return "此信用卡已被掛失";
                case "1292":
                    return "此信用卡已被停卡";
                case "1293":
                    return "信用卡驗證碼 (CVN) 無效";
                case "1294":
                    return "此信用卡已被列入黑名單";
                case "1295":
                    return "信用卡號無效";
                case "1296":
                    return "無效的金額";
                case "1298":
                    return "信用卡付款遭拒";
                case "2101":
                    return "參數錯誤";
                case "2102":
                    return "JSON 資料格式錯誤";
                case "9000":
                    return "內部錯誤";
                default:
                    return null;
            }
        }

        /// <summary>
        /// 結果訊息或失敗理由。範例如下：
        /// 商家未獲授權，無法付款
        /// 商家驗證資訊錯誤
        /// </summary>
        [JsonProperty("returnMessage")]
        public string ReturnMessage { get; set; }

        [JsonProperty("info")]
        public T Info { get; set; }
    }
}
