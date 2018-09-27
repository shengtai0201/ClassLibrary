using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway
{
    /// <summary>
    /// Samaung Pay支付回傳參數
    /// </summary>
    /// <seealso cref="Shengtai.Web.Spgateway.ResponseResult" />
    public interface IResponseSamaungPayResult : IResponseResult
    {
        /// <summary>
        /// 金融機構回應碼 
        /// </summary>
        /// <value>
        /// 1.由收單機構所回應的回應碼。 
        /// 2.若交易送至收單機構授權時已是失敗狀態，則本欄位的值會以空值回傳。 
        /// </value>
        [StringLength(5)]
        string RespondCode { get; set; }

        /// <summary>
        /// 授權碼 
        /// </summary>
        /// <value>
        /// 1.由收單機構所回應的授權碼。 
        /// 2.若交易送至收單機構授權時已是失敗狀態，則本欄位的值會以空值回傳。 
        /// </value>
        [StringLength(6)]
        string Auth { get; set; }

        /// <summary>
        /// 卡號前六碼 
        /// </summary>
        /// <value>
        /// 1.信用卡卡號前六碼。 
        /// 2.若交易送至收單機構授權時已是失敗狀態，則本欄位的值會以空值回傳。 
        /// </value>
        [StringLength(6)]
        string Card6No { get; set; }

        /// <summary>
        /// 卡號末四碼 
        /// </summary>
        /// <value>
        /// 1.信用卡卡號後四碼。 
        /// 2.若交易送至收單機構授權時已是失敗狀態，則本欄位的值會以空值回傳。 
        /// </value>
        [StringLength(4)]
        string Card4No { get; set; }

        /// <summary>
        /// 分期-期別 
        /// </summary>
        /// <value>
        /// 信用卡分期交易期別。 
        /// </value>
        [StringLength(10)]
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
        /// ECI 值 
        /// </summary>
        /// <value>
        /// 1.3D 回傳值 eci=1,2,5,6，代表為 3D 交易。 
        /// 2.若交易送至收單機構授權時已是失敗狀態，則本欄位的值會以空值回傳。 
        /// </value>
        [StringLength(2)]
        string ECI { get; set; }

        /// <summary>
        /// 信用卡快速結帳 使用狀態 
        /// </summary>
        /// <value>
        /// 0=該筆交易為非使用信用卡快速結帳功能 
        /// 1=該筆交易為首次設定信用卡快速結帳功能 
        /// 2=該筆交易為使用信用卡快速結帳功能 
        /// 9=該筆交易為取消信用卡快速結帳功能功能
        /// </value>
        [StringLength(1)]
        int TokenUseStatus { get; set; }

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
        [StringLength(5)]
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
        [StringLength(5)]
        PaymentMethods PaymentMethod { get; set; }
    }
}
