using NUnit.Framework;
using Shengtai.Web.Spgateway;
using Shengtai.Web.Spgateway.QueryTradeInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Tests
{
    [TestFixture]
    public class SpgatewayQueryTradeInfoTest
    {
        private readonly string url;
        private readonly string key;
        private readonly string iv;

        public SpgatewayQueryTradeInfoTest()
        {
            this.url = "https://ccore.spgateway.com/MPG/mpg_gateway";
            this.key = "abcdefg";
            this.iv = "1234567";
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }

        [Test]
        public void RequestTest()
        {
            // assign
            var crypto = new Web.Spgateway.QueryTradeInfo.Cryptography(this.key, this.iv);
            var trade = new RequestTrade
            {
                MerchantID = "1422967",
                //Version = "1.1",
                //RespondType = "JSON",
                //TimeStamp = "1485232229",
                MerchantOrderNo = "840f022",
                Amt = 100
            };

            // act
            var tradeInfo = crypto.GetTradeInfo(trade);
            var actual = crypto.GetTradeSha(tradeInfo);

            // assert
            var expected = "379BF1DB8948EE79D8ED77A1EBCB2F57B0FD45D0376B6DA9CF85F539CEF1C127";
            Assert.AreEqual(expected, actual);
        }

        // todo: 驗證 CheckCode 的部分不應該在轉成物件後才作驗證，須在網路傳遞後，在 json object 中驗證
        [Test]
        public void ResponseTest()
        {
            // assign
            var crypto = new Web.Spgateway.QueryTradeInfo.Cryptography(this.key, this.iv);
            var result = new ResponseResult
            {
                MerchantID = "1422967",
                MerchantOrderNo = "840f022",
                Amt = 100,
                TradeNo = "14061313541640927"
            };

            // act
            var tradeInfo = crypto.GetTradeInfo(result);
            var actual = crypto.GetTradeSha(tradeInfo);

            // assert
            var expected = "62C687AF6409E46E79769FAF54F54FE7E75AAE50BAF0767752A5C337670B8EDB";
            Assert.AreEqual(expected, actual);
        }
    }
}
