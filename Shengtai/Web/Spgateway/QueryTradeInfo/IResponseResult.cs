using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway.QueryTradeInfo
{
    /// <summary>
    /// 回傳內容
    /// </summary>
    public interface IResponseResult
    {
        /// <summary>
        /// 商店代號
        /// </summary>
        /// <value>
        /// 商店代號
        /// </value>
        [StringLength(15)]
        string MerchantID { get; set; }

        /// <summary>
        /// 交易金額
        /// </summary>
        /// <value>
        /// 查詢此筆訂單之交易金額
        /// </value>
        [StringLength(15)]
        int Amt { get; set; }

        /// <summary>
        /// 智付通交易序號
        /// </summary>
        /// <value>
        /// 此次執行查詢的商店訂單所對應之智付通交易序號
        /// </value>
        [StringLength(20)]
        string TradeNo { get; set; }

        /// <summary>
        /// 商店訂單編號
        /// </summary>
        /// <value>
        /// 此次執行查詢的商店訂單編號
        /// </value>
        [StringLength(20)]
        string MerchantOrderNo { get; set; }

        /// <summary>
        /// 支付狀態
        /// </summary>
        /// <value>
        /// 以數字回應，其代表下列意含： 
        ///     0=未付款 
        ///     1=付款成功 
        ///     2=付款失敗 
        ///     3=取消付款
        /// </value>
        [StringLength(1)]
        int TradeStatus { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        /// <value>
        /// 以英文代號方式回應： 
        ///     CREDIT=信用卡付款 
        ///     VACC=銀行ATM轉帳付款 
        ///     WEBATM=網路銀行轉帳付款 
        ///     BARCODE=超商條碼繳費 
        ///     CVS=超商代碼繳費
        /// </value>
        [StringLength(10)]
        PaymentTypes PaymentType { get; set; }

        /// <summary>
        /// 交易建立時間
        /// </summary>
        /// <value>
        /// 智付通接收到此筆交易資料的時間，回傳格式為：2014-06-2516:43:49
        /// </value>
        DateTime CreateTime { get; set; }

        /// <summary>
        /// 支付時間
        /// </summary>
        /// <value>
        /// 智付通接收到此筆交易付款完成資訊的時間，回傳格式為：2014-06-2516:43:49
        /// </value>
        DateTime PayTime { get; set; }

        /// <summary>
        /// 檢核碼
        /// </summary>
        /// <value>
        /// 用來檢查此次資料回傳的合法性，商店查詢時可以比對此欄位資料，檢核是否為智付通平台所回傳，檢核方法請參考附件二
        /// </value>
        string CheckCode { get; set; }

        /// <summary>
        /// 預計撥款日
        /// </summary>
        /// <value>
        /// 預計撥款的時間，回傳格式為：2014-06-25
        /// </value>
        DateTime FundTime { get; set; }
    }
}
