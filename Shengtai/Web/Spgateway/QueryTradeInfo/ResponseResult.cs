using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway.QueryTradeInfo
{
    public class ResponseResult : IResponseCreditResult, IResponseCBVResult
    {
        #region 回傳內容
        public string MerchantID { get; set; }
        public int Amt { get; set; }
        public string TradeNo { get; set; }
        public string MerchantOrderNo { get; set; }
        public int TradeStatus { get; set; }
        public PaymentTypes PaymentType { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime PayTime { get; set; }
        public string CheckCode { get; set; }
        public DateTime FundTime { get; set; }
        #endregion

        #region 信用卡專屬欄位
        public string RespondCode { get; set; }
        public string Auth { get; set; }
        public string ECI { get; set; }
        public int CloseAmt { get; set; }
        public int CloseStatus { get; set; }
        public int BackBalance { get; set; }
        public int BackStatus { get; set; }
        public string RespondMsg { get; set; }
        public int Inst { get; set; }
        public int InstFirst { get; set; }
        public int InstEach { get; set; }
        public int Bonus { get; set; }
        public int RedAmt { get; set; }
        public PaymentMethods PaymentMethod { get; set; }
        #endregion

        #region 超商代碼 、超商條碼、ATM轉帳 專屬欄位
        public string PayInfo { get; set; }
        public DateTime ExpireDate { get; set; }
        #endregion
    }
}
