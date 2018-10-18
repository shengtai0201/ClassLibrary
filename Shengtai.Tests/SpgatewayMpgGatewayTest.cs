using NUnit.Framework;
using Shengtai.Web.Spgateway;
using Shengtai.Web.Spgateway.MpgGateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Tests
{
    [TestFixture]
    public class SpgatewayMpgGatewayTest
    {
        private readonly string url;
        private readonly string key;
        private readonly string iv;

        public SpgatewayMpgGatewayTest()
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
            var crypto = new Web.Spgateway.MpgGateway.Cryptography(this.key, this.iv);
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
            var crypto = new Web.Spgateway.MpgGateway.Cryptography(this.key, this.iv);
            var tradeInfo = "ff91c8aa01379e4de621a44e5f11f72e4d25bdb1a18242db6cef9ef07d80b0165e476fd1d9acaa53170272c82d122961e1a0700a7427cfa1cf90db7f6d6593bbc93102a4d4b9b66d9974c13c31a7ab4bba1d4e0790f0cbbbd7ad64c6d3c8012a601ceaa808bff70f94a8efa5a4f984b9d41304ffd879612177c622f75f4214fa";

            // act
            var actual = crypto.GetTradeSha(tradeInfo);

            // assert
            var expected = "EA0A6CC37F40C1EA5692E7CBB8AE097653DF3E91365E6A9CD7E91312413C7BB8";
            Assert.AreEqual(expected, actual);
        }

        // 即時交易支付方式：信用卡(CREDIT)、WebATM(WEBATM) 、Pay2go 電子錢包(P2G)、
        // Google Pay(ANDROIDPAY)、Samsung Pay(SAMSUNGPAY)
        [Test]
        public void ResponseTest1()
        {
            // assign
            Response response = new Response
            {
                Status = "SUCCESS",
                MerchantID = "3430112",
                TradeInfo = "3716aa2a929c1b40071a732f1b8c57c3ecb6a6aeefed4061705dba662c14d0ecd5ae3aa9ed14e96745b3542fdda87c89ad4e9909e9b7f8d05288dae032526e7e0825b39b9e4df97b7b70e50866a71b6f9e17c6514e56cf5e3befee9c3371df308e3554e0713c07f7042818f24b8100ccc3728e2b590310416eff8a53a9d807d94a1df600f76c6931afab103ffc9e551d54974d59b3a54f255fed4062fb5da8470f127077979c60070a6a6f1793cddf3d34c521817ba26e78394dec16cbda8ded9b21f977be42ad4946ec9c4481e130ef812888df401cb0ab40d4f8348f848b6f5f4473184dd6a0d86916b3ccb1198eee41014554196628c74a22e7d7e3183cd8c2270a1236fed34102f7c71fb44913d5edd87f1b5432d3532786c83ad12fc341a2c45e9b8294605e50059d3792d7a6cb76ecaab7e6d0209ea65cc5bb2371ef177a5e869d0ae306fce2210b59bf90c6f6e7cf4807d069c1fed3bb199eb89f25556aebbe0dd8d384481c452a807762685537239fff06e2bf5bd3fb1d612e55fceb34b1425e458ba5b6b946ba943a7e42598f2f0a9713d8cf2327a2bd0d7d7952053f109ea896d2c88c4f1fc7680ff12af9877a9c3e888367d65fdb69d13b3e7a3f955ef1e286ae94a183652b4c36b3639ba39d3affb1833feef1797874a61b57bc3247e4cb9b30532a7c92b6997e2476e9b90aebaab2367a25ff1fa33f1b380c772822fea4f34b7b8002d537e8944e91a9045c328e21eaaec80fbbb5d6bbf067e7936f18a1ecd1e3a0c8db44c8215e8ba2624894ef4c31fab41e537157b206a0a7ea26ad326aae5b07856b3e7be32c0f0dddd4e8275f09d90a2233aecb0470409b7d67b6d4b6778faa7dd26f6143130786b8a4cfc9b05ae76976b709474c34d409605cd32c4c5e0f6c341349efc2ed002fdf74f35a559fff6dd1dc4c138629bc02b96edb06c48cf6b4976f5379576a2eec897094a2460781ea251859b72a2fc3339ff66a56c8737e9a79ad25ae9e4cd8febc5c9ac1632e8706bcf4b0dda66ff9f6",
                TradeSha = "3052314E7E9A6418128705697ECA25DE3381FF603B9614291DBD9F19FF93F8A9"
            };

            // act
            var actual = new Web.Spgateway.MpgGateway.Cryptography(this.key, this.iv).GetResult(response.TradeInfo);
        }

        // 非即時交易支付方式：超商代碼繳費(CVS)、ATM 轉帳(VACC)、
        // 超商條碼繳費(BARCODE) 、超商取貨付款(CVSCOM)
        [Test]
        public void ResponseTest2()
        {
            // assign
            Response response = new Response
            {
                Status = "SUCCESS",
                MerchantID = "3430112",
                TradeInfo = "545c49e585b9ed22e5b4ecf4061a362fec3d1a449ee315ad86405e6f32a3b71f27d2f910ba42b49d4768f843349f7c99497ef4d53616a64bf85d8550b459587dfd8b0104a1642cb742e3becb6094b1b7351182575db14fc0b90bd07df5f831e5682f1d40e9c481419c70f1e36265c8cfe127bbe0b9904d8a8e8f166b56a6e4702a31b7453615f2c5c49f11dac2362a8f4248b9f5f480d59d43f42e40e6c509ba761cbdddeb8f1fbf9e5396150984de6bcc3389a614e918758b6828774d953a3d5105bd8777aa81d3639dd0cf656280ce4c2e0d5bef75c3d02ea1533c64bfeab6a99545e83044692130fc45abcae17fcc66d22fa676e24086664636e54de70df302c711515db10049d97a8d4ec31d586668d84c87ab53ea3994a0af0a6b50f544122f592d89f45f1f081c74b15cf1faeb05c27ba61548ca410a6f56a9766316825ce16729a3061473eeaf023d105b1d5d02aa6ad3b56525925609ec2a958a9d77",
                TradeSha = "5615081069F6A40563E4704B149FD1AEF63964CE19C431BD27E2F4C1DD2A6484"
            };

            // act
            var actual = new Web.Spgateway.MpgGateway.Cryptography(this.key, this.iv).GetResult(response.TradeInfo);
        }
    }
}
