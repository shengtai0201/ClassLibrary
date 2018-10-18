using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway.QueryTradeInfo
{
    /// <summary>
    /// 信用卡專屬欄位： 當該筆交易為信用卡時 （包含：國外卡、國旅卡、ApplePay、 GooglePay、SamsungPay）
    /// </summary>
    /// <seealso cref="Shengtai.Web.Spgateway.QueryTradeInfo.IResponseResult" />
    public interface IResponseCreditResult : IResponseResult
    {
        /// <summary>
        /// 金融機構回應碼 
        /// </summary>
        /// <value>
        /// 1.由收單機構所回應的回應碼。 
        /// 2.若交易送至收單機構授權時已是失敗狀態，則本欄位的值會以空值回傳。 
        /// </value>
        [StringLength(10)]
        string RespondCode { get; set; }

        /// <summary>
        /// 授權碼 
        /// </summary>
        /// <value>
        /// 1.由收單機構所回應的授權碼。 
        /// 2.若交易送至收單機構授權時已是失敗狀態，則本欄位的值會以空值回傳。 
        /// </value>
        [StringLength(10)]
        string Auth { get; set; }

        /// <summary>
        /// ECI 值 
        /// </summary>
        /// <value>
        /// 1.3D 回傳值 eci=1,2,5,6，代表為 3D 交易。 
        /// 2.若交易送至收單機構授權時已是失敗狀態，則本欄位的值會以空值回傳。 
        /// </value>
        [StringLength(1)]
        string ECI { get; set; }

        /// <summary>
        /// 請款金額
        /// </summary>
        /// <value>
        /// 此筆交易設定的請款金額
        /// </value>
        [StringLength(10)]
        int CloseAmt { get; set; }

        /// <summary>
        /// 請款狀態
        /// </summary>
        /// <value>
        /// 請款狀態依不同情況，設定值代表下列意含： 
        ///     0=未請款 
        ///     1=等待提送請款至收單機構 
        ///     2=請款處理中 
        ///     3=請款完成
        /// </value>
        [StringLength(1)]
        int CloseStatus { get; set; }

        /// <summary>
        /// 可退款餘額
        /// </summary>
        /// <value>
        /// 此筆交易尚可退款餘額，若此筆交易未請款則此處金額為0
        /// </value>
        [StringLength(11)]
        int BackBalance { get; set; }

        /// <summary>
        /// 退款狀態
        /// </summary>
        /// <value>
        /// 退款狀態依不同情況，設定值代表下列意含 
        ///     0=未退款 
        ///     1=等待提送退款至收單機構 
        ///     2=退款處理中 
        ///     3=退款完成
        /// </value>
        [StringLength(1)]
        int BackStatus { get; set; }

        /// <summary>
        /// 授權結果訊息
        /// </summary>
        /// <value>
        /// 文字，銀行回覆此次信用卡授權結果狀態。
        /// </value>
        [StringLength(50)]
        string RespondMsg { get; set; }

        /// <summary>
        /// 分期-期別 
        /// </summary>
        /// <value>
        /// 信用卡分期交易期別。 
        /// </value>
        [StringLength(3)]
        int Inst { get; set; }

        /// <summary>
        /// 分期-首期金額 
        /// </summary>
        /// <value>
        /// 信用卡分期交易首期金額。 
        /// </value>
        [StringLength(10)]
        int InstFirst { get; set; }

        /// <summary>
        /// 分期-每期金額 
        /// </summary>
        /// <value>
        /// 信用卡分期交易每期金額。 
        /// </value>
        [StringLength(10)]
        int InstEach { get; set; }

        /// <summary>
        /// 紅利交易類別
        /// </summary>
        /// <value>
        /// 此筆信用卡交易是否為使用紅利折抵之交易： 
        ///     0=一般交易 
        ///     1=紅利交易
        /// </value>
        [StringLength(1)]
        int Bonus { get; set; }

        /// <summary>
        /// 紅利折抵後實際金額
        /// </summary>
        /// <value>
        /// 1.扣除紅利交易折抵後的實際授權金額。 
        /// 例：1000 元之交易，紅利折抵 60 元，則紅利折抵後 實際金額為 940 元。 
        /// 2.若紅利點數不足，會有以下狀況： 
        ///     2-1 紅利折抵交易失敗，回傳參數數值為 0。 
        ///     2-2 紅利折抵交易成功，回傳參數數值為訂單金額。 
        ///     2-3 紅利折抵交易是否成功，視該銀行之設定為準。 
        /// 3.僅有使用紅利折抵交易時才會回傳此參數。 
        /// </value>
        [StringLength(15)]
        int RedAmt { get; set; }

        /// <summary>
        /// 交易類別
        /// </summary>
        /// <value>
        /// 將依據此筆交易之信用卡類別回傳相對應的參數，對應參數如下： 
        ///     CREDIT = 台灣發卡機構核發之信用卡
        ///     FOREIGN = 國外發卡機構核發之卡
        ///     UNIONPAY = 銀聯卡
        ///     GOOGLEPAY = GooglePay
        ///     SAMSUNGPAY = SamsungPay
        /// </value>
        [StringLength(15)]
        PaymentMethods PaymentMethod { get; set; }
    }
}
