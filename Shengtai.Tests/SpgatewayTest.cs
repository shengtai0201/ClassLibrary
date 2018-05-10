using NUnit.Framework;
using Shengtai.Web.Spgateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Tests
{
    [TestFixture]
    public class SpgatewayTest
    {
        private string url;
        private string key;
        private string iv;

        public SpgatewayTest()
        {
            this.url = "https://ccore.spgateway.com/MPG/mpg_gateway";
            this.key = "12345678901234567890123456789012";
            this.iv = "1234567890123456";
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {

        }

        [Test]
        public void ToUnixTimeStampTest()
        {
            var dateTime = new DateTime(2014, 5, 15, 15, 0, 0);
            var actual = dateTime.AddHours(-8).ToUnixTimeStamp();

            Assert.AreEqual("1400137200", actual);
        }

        [Test]
        public void GetTradeInfoTest()
        {
            // assign
            var crypto = new Crypto(this.key, this.iv);
            var trade = new RequestTrade
            {
                MerchantID = "3430112",
                RespondType = "JSON",
                TimeStamp = "1485232229",
                Version = "1.4",
                MerchantOrderNo = "S_1485232229",
                Amt = 40,
                ItemDesc = "UnitTest"
            };

            // act
            var actual = crypto.GetTradeInfo(trade);

            // assert
            var expected = "ff91c8aa01379e4de621a44e5f11f72e4d25bdb1a18242db6cef9ef07d80b0165e476fd1d9acaa53170272c82d122961e1a0700a7427cfa1cf90db7f6d6593bbc93102a4d4b9b66d9974c13c31a7ab4bba1d4e0790f0cbbbd7ad64c6d3c8012a601ceaa808bff70f94a8efa5a4f984b9d41304ffd879612177c622f75f4214fa";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTradeShaTest()
        {
            // assign
            var crypto = new Crypto(this.key, this.iv);
            var tradeInfo = "ff91c8aa01379e4de621a44e5f11f72e4d25bdb1a18242db6cef9ef07d80b0165e476fd1d9acaa53170272c82d122961e1a0700a7427cfa1cf90db7f6d6593bbc93102a4d4b9b66d9974c13c31a7ab4bba1d4e0790f0cbbbd7ad64c6d3c8012a601ceaa808bff70f94a8efa5a4f984b9d41304ffd879612177c622f75f4214fa";

            // act
            var actual = crypto.GetTradeSha(tradeInfo);

            // assert
            var expected = "EA0A6CC37F40C1EA5692E7CBB8AE097653DF3E91365E6A9CD7E91312413C7BB8";
            Assert.AreEqual(expected, actual);
        }

        public void Get

        [Test]
        public void AAA()
        {
            // assign
            var crypto = new Crypto(this.key, this.iv);
            var trade = new RequestTrade
            {
                MerchantID = "3430112",
                RespondType = "JSON",
                TimeStamp = "1485232229",
                Version = "1.4",
                MerchantOrderNo = "S_1485232229",
                Amt = 40,
                ItemDesc = "UnitTest",
                CREDIT = 1
            };
        }
    }
}
