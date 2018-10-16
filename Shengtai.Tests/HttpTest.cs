using NUnit.Framework;
using Shengtai.Web.Spgateway.MpgGateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Tests
{
    [TestFixture]
    public class HttpTest
    {
        private readonly string url;
        private readonly string key;
        private readonly string iv;

        public HttpTest()
        {
            this.url = "https://ccore.spgateway.com/MPG/mpg_gateway";
            this.key = "nbR1iU7GSxgmVQ2IAH1REjK2X6VdSXiF";
            this.iv = "n2Ii6Q6lumqHVhYD";
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }

        [Test]
        public void TradeTest()
        {
            // assign
            var crypto = new Web.Spgateway.MpgGateway.Cryptography(this.key, this.iv);
            var trade = new RequestTrade
            {
                MerchantID = "MS34923336",
                TimeStamp = "1538448563",
                Version = "1.2",
                MerchantOrderNo = "1538448563",
                Amt = 65
            };

            // act
            var tradeInfo = crypto.GetTradeInfo(trade);
            var actual = crypto.GetTradeSha(tradeInfo);

            // assert
            var expected = "BF1CA9087E20A89CBC600806883A037F66C894BAF037A08E22AB6FA244A7A04D";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        //public async Task AAA()
        public void AAA()
        {
            var crypto = new Shengtai.Web.Spgateway.MpgGateway.Cryptography(this.key, this.iv);
            var trade = new RequestTrade
            {
                MerchantID = "MS34923336",
                //RespondType = "JSON",
                //TimeStamp = DateTime.Now.AddHours(-8).ToUnixTimeStamp().ToString(),
                TimeStamp = "1538449466",
                Version = "1.4",
                //MerchantOrderNo = "999",
                MerchantOrderNo= "1538449466",
                //Amt = 300,
                Amt = 78//,
                //ItemDesc = "線上聲請強制執行，入口網站帳號啟用開辦費用。",
                //Email = "shengtai@loansky.net",
                //LoginType = 0
            };

            //var tradeInfo = crypto.GetTradeInfo(trade);
            //var actual = crypto.GetTradeSha(tradeInfo);
            //var expected = "C61CA75BC7F009F4F581CD240F78DFBF3C662D63CB4A89F439741B60C528BE59";
            //Assert.AreEqual(expected, actual);

            var request = Request.NewInstance(trade, crypto);
            //var response = await Shengtai.Http.DefaultExtensions.Post<Response>(this.url, request);
            var result = Shengtai.Http.DefaultExtensions.HttpPost(this.url, request);
            var test = string.Empty;
        }
    }
}