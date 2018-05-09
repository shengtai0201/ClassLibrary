using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway
{
    public class ResponseResult : IResponseCreditResult, IResponseWebatmResult, IResponseVaccResult, IResponseCvsResult, 
        IResponseBarcodeResult, IResponseP2gResult, IResponseCvscomResult
    {
        #region 所有支付方式共同回傳參數
        public string MerchantID { get; set; }
        public int Amt { get; set; }
        public string TradeNo { get; set; }
        public string MerchantOrderNo { get; set; }
        public PaymentTypes PaymentType { get; set; }
        public string RespondType { get; set; }
        public DateTime PayTime { get; set; }
        public string IP { get; set; }
        public string EscrowBank { get; set; }
        #endregion

        #region 信用卡支付回傳參數
        public string RespondCode { get; set; }
        public string Auth { get; set; }
        public string Card6No { get; set; }
        public string Card4No { get; set; }
        public int Inst { get; set; }
        public int InstFirst { get; set; }
        public int InstEach { get; set; }
        public string ECI { get; set; }
        public int TokenUseStatus { get; set; }
        public int RedAmt { get; set; }
        #endregion

        #region WEBATM、ATM 繳費回傳參數
        public string PayBankCode { get; set; }
        public int PayerAccount5Code { get; set; }
        #endregion

        #region 超商代碼繳費回傳參數 
        public string CodeNo { get; set; }
        public object StoreType { get; set; }
        public string StoreID { get; set; }
        #endregion

        #region 超商條碼繳費回傳參數 
        public string Barcode_1 { get; set; }
        public string Barcode_2 { get; set; }
        public string Barcode_3 { get; set; }
        public string PayStore { get; set; }
        #endregion

        #region Pay2go 電子錢包回傳參數 
        public string P2GTradeNo { get; set; }
        public P2GPaymentTypes P2GPaymentType { get; set; }
        public int P2GAmt { get; set; }
        #endregion

        #region 超商物流回傳參數 
        public string StoreName { get; set; }
        public string StoreAddr { get; set; }
        public int TradeType { get; set; }
        public string CVSCOMName { get; set; }
        public string CVSCOMPhone { get; set; }
        #endregion
    }
}
