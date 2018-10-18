using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway.MpgGateway
{
    public class ResponseResultCode : IResponseVaccResultCode, IResponseCvsResultCode, IResponseBarcodeResultCode
    {
        #region ATM 轉帳、超商代碼繳費、超商條碼繳費共同回傳參數
        public string MerchantID { get; set; }
        public int Amt { get; set; }
        public string TradeNo { get; set; }
        public string MerchantOrderNo { get; set; }
        public PaymentTypes PaymentType { get; set; }
        public DateTime ExpireDate { get; set; }
        #endregion

        #region ATM 轉帳回傳參數 
        public string BankCode { get; set; }
        public string CodeNo { get; set; }
        #endregion

        #region 超商條碼繳費回傳參數 
        public string Barcode_1 { get; set; }
        public string Barcode_2 { get; set; }
        public string Barcode_3 { get; set; }
        #endregion
    }
}
