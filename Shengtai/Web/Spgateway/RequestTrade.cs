using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway
{
    /// <summary>
    /// 交易資料參數
    /// </summary>
    public class RequestTrade
    {
        /// <summary>
        /// 商店代號
        /// </summary>
        /// <value>
        /// 智付通商店代號。
        /// </value>
        [StringLength(15)]
        [Required]
        public string MerchantID { get; set; }

        /// <summary>
        /// 回傳格式
        /// </summary>
        /// <value>
        /// JSON 或是 String。
        /// </value>
        [StringLength(6)]
        [Required]
        public string RespondType { get; set; }// = "JSON";

        /// <summary>
        /// 時間戳記 
        /// </summary>
        /// <value>
        /// 自從 Unix 纪元（格林威治時間 1970 年 1 月 1 日 00:00:00）到當前時間的秒數，若 以 php 程式語言為例，即為呼叫 time()函式 所回傳的值。
        /// 例：2014-05-15 15:00:00(+08:00 時區)這個時間的時間戳記為 1400137200。 
        /// </value>
        [StringLength(50)]
        [Required]
        public string TimeStamp { get; set; }

        /// <summary>
        /// 串接程式版本 
        /// </summary>
        /// <value>
        /// 請帶 1.4。 
        /// </value>
        [StringLength(5)]
        [Required]
        public string Version { get; set; }// = "1.4";

        /// <summary>
        /// 語系 
        /// </summary>
        /// <value>
        /// 1.設定 MPG 頁面顯示的文字語系。英文版參數為 en 繁體中文版參數為 zh-tw
        /// 2.當未提供此參數或此參數數值錯誤時，將預設為繁體中文版。 
        /// </value>
        [StringLength(5)]
        public string LangType { get; set; }// = "zh-tw";

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
        public int Amt { get; set; }

        /// <summary>
        /// 商品資訊 
        /// </summary>
        /// <value>
        /// 1.限制長度為 50 字。 
        /// 2.編碼為 Utf-8 格式。 
        /// </value>
        [StringLength(50)]
        [Required]
        public string ItemDesc { get; set; }

        /// <summary>
        /// 交易限制秒數 
        /// </summary>
        /// <value>
        /// 1.限制交易的秒數，當秒數倒數至 0 時，交易當做失敗。 
        /// 2.僅可接受數字格式。 
        /// 3.秒數下限為 60 秒，當秒數介於 1~59 秒時，會以 60 秒計算。 
        /// 4.秒數上限為 900 秒，當超過 900 秒時，會以 900 秒計算。 
        /// 5.若未帶此參數，或是為 0 時，會視作為不啟用交易限制秒數。 
        /// </value>
        [StringLength(3)]
        public int? TradeLimit { get; set; }

        /// <summary>
        /// 繳費有效期限(適用於非即時交易)
        /// </summary>
        /// <value>
        /// 1.格式為 date('Ymd') ，例：20140620 
        /// 2.此參數若為空值，系統預設為 7 天。自取號時間起算至第 7 天 23:59:59。 
        /// 例：2014-06-23 14:35:51 完成取號，則繳費有效期限為 2014-06-29 23:59:59。 3.可接受最大值為 180 天。 
        /// </value>
        [StringLength(10)]
        public string ExpireDate { get; set; }

        /// <summary>
        /// 支付完成返回商店網址 
        /// </summary>
        /// <value>
        /// 1.交易完成後，以 Form Post 方式導回商店頁面。 
        /// 2.若為空值，交易完成後，消費者將停留在智付通付款或取號完成頁面。 3.只接受 80 與 443 Port。 
        /// </value>
        [StringLength(50)]
        public string ReturnURL { get; set; }

        /// <summary>
        /// 支付通知網址 
        /// </summary>
        /// <value>
        /// 1.以幕後方式回傳給商店相關支付結果資料；請參考六、交易支付系統回傳參數說 明。 
        /// 2. 只接受 80 與 443 Port。
        /// </value>
        [StringLength(50)]
        public string NotifyURL { get; set; }

        /// <summary>
        /// 商店取號網址 
        /// </summary>
        /// <value>
        /// 1.系統取號後以 form post 方式將結果導回商店指定的網址，請參考 七、取號完成系統回傳參數說明。 
        /// 2.此參數若為空值，則會顯示取號結果在智付通頁面。 
        /// </value>
        [StringLength(50)]
        public string CustomerURL { get; set; }

        /// <summary>
        /// 支付取消返回商店網址
        /// </summary>
        /// <value>
        /// 1.當交易取消時，平台會出現返回鈕，使消費者依以此參數網址返回商店指定的頁面。 
        /// 2.此參數若為空值時，則無返回鈕。 
        /// </value>
        [StringLength(50)]
        public string ClientBackURL { get; set; }

        /// <summary>
        /// 付款人電子信箱 
        /// </summary>
        /// <value>
        /// 於交易完成或付款完成時，通知付款人使用。
        /// </value>
        [StringLength(50)]
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// 付款人電子信箱是否開放修改
        /// </summary>
        /// <value>
        /// 1.設定於 MPG 頁面，付款人電子信箱欄位 是否開放讓付款人修改。   
        ///     1 = 可修改   
        ///     0 = 不可修改 
        /// 2.當未提供此參數時，將預設為可修改。 
        /// </value>
        [StringLength(1)]
        public int? EmailModify { get; set; }

        /// <summary>
        /// 智付通會員 
        /// </summary>
        /// <value>
        ///  1 = 須要登入智付通會員   
        ///  0 = 不須登入智付通會員 
        /// </value>
        [StringLength(1)]
        [Required]
        public int? LoginType { get; set; }

        /// <summary>
        /// 商店備註 
        /// </summary>
        /// <value>
        /// 1.限制長度為 300 字。 
        /// 2.若有提供此參數，將會於 MPG 頁面呈現 商店備註內容。 
        /// </value>
        [StringLength(300)]
        public string OrderComment { get; set; }

        /// <summary>
        /// 信用卡一次付清啟用 
        /// </summary>
        /// <value>
        /// 1.設定是否啟用信用卡一次付清支付方式。   
        ///     1 =啟用   
        ///     0 或者未有此參數=不啟用 
        /// </value>
        [StringLength(1)]
        public int? CREDIT { get; set; }

        /// <summary>
        /// 信用卡分期付款啟用 
        /// </summary>
        /// <value>
        /// 1.此欄位值=1 時，即代表開啟所有分期期 別，且不可帶入其他期別參數。 
        /// 2.此欄位值為下列數值時，即代表開啟該分 期期別。  
        ///     3=分 3 期功能  
        ///     6=分 6 期功能  
        ///     12=分 12 期功能  
        ///     18=分 18 期功能  
        ///     24=分 24 期功能   
        ///     30=分 30 期功能 
        /// 3.同時開啟多期別時，將此參數用”，”(半 形)分隔，例如：3,6,12，代表開啟 分 3、 6、12 期的功能。 
        /// 4. 此欄位值=０或無值時，即代表不開啟分 期。 
        /// </value>
        [StringLength(18)]
        public int? InstFlag { get; set; }

        /// <summary>
        /// 信用卡紅利啟用 
        /// </summary>
        /// <value>
        /// 1.設定是否啟用信用卡紅利支付方式。   
        ///     1 =啟用   
        ///     0 或者未有此參數=不啟用 
        /// </value>
        [StringLength(1)]
        public int? CreditRed { get; set; }

        /// <summary>
        /// 信用卡銀聯卡啟用 
        /// </summary>
        /// <value>
        /// 1.設定是否啟用銀聯卡支付方式。 
        ///     1=啟用  
        ///     0 或者未有此參數=不啟用 
        /// </value>
        [StringLength(1)]
        public int? UNIONPAY { get; set; }

        /// <summary>
        /// WEBATM 啟用
        /// </summary>
        /// <value>
        /// 1.設定是否啟用 WEBATM 支付方式。   
        ///     1=啟用   
        ///     0 或者未有此參數，即代表不開啟。 
        /// </value>
        [StringLength(1)]
        public int? WEBATM { get; set; }

        /// <summary>
        /// ATM 轉帳啟用 
        /// </summary>
        /// <value>
        /// 1.設定是否啟用 ATM 轉帳支付方式。 
        ///     1 = 啟用 
        ///     0 或者未有此參數，即代表不開啟。 
        /// </value>
        [StringLength(1)]
        public int? VACC { get; set; }

        /// <summary>
        /// 超商代碼繳費啟用
        /// </summary>
        /// <value>
        /// 1.設定是否啟用超商代碼繳費支付方式   
        ///     1 = 啟用 
        ///     0 或者未有此參數，即代表不開啟。 
        /// 2.當該筆訂單金額小於 30 元或超過 2 萬元 時，即使此參數設定為啟用，MPG 付款頁 面仍不會顯示此支付方式選項。
        /// </value>
        [StringLength(1)]
        public int? CVS { get; set; }

        /// <summary>
        /// 超商條碼繳費啟 用 
        /// </summary>
        /// <value>
        /// 1.設定是否啟用超商條碼繳費支付方式 
        ///     1 = 啟用   
        ///     0 或者未有此參數，即代表不開啟。 
        /// 2.當該筆訂單金額小於 20 元或超過 4 萬元 時，即使此參數設定為啟用，MPG 付款頁 面仍不會顯示此支付方式選項。 
        /// </value>
        [StringLength(1)]
        public int? BARCODE { get; set; }

        /// <summary>
        /// Pay2go 電子錢 包啟用 
        /// </summary>
        /// <value>
        /// 1.設定是否啟用 Pay2go 電子錢包支付方 式。 
        ///     1 = 啟用 
        ///     0 或者未有此參數，即代表不開啟。 
        /// </value>
        [StringLength(1)]
        public int? P2G { get; set; }

        /// <summary>
        /// 物流啟用 
        /// </summary>
        /// <value>
        /// 1.使用前，須先登入智付通會員專區啟用物 流並設定退貨門市與取貨人相關資訊。   
        ///     1 = 超商取貨不付款   
        ///     2 = 超商取貨付款   
        ///     3 = 超商取貨不付款及超商取貨付款   
        ///     0 或者未有此參數，即代表不開啟。 
        /// 2.當該筆訂單金額小於 30 元或大於 2 萬元 時，即使此參數設定為啟用，MPG 付款頁 面仍不會顯示此支付方式選項。 
        /// </value>
        [StringLength(1)]
        public int? CVSCOM { get; set; }

        //public RequestTrade()
        //{
        //    //this.Version = "1.4";
        //    //this.LangType = "zh-tw";
        //    //this.ExpireDate = DateTime.Now.AddDays(6).ToString("yyyyMMdd");
        //    //this.EmailModify = 1;
        //}
    }
}
