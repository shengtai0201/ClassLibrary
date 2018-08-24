using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Shengtai.Cryptography
{
    public static class Pkcs
    {
        public static IDictionary<string, string> VerifyData(Pkcs1 data, out byte[] buffer)
        {
            IDictionary<string, string> result = null;

            var x509 = new X509Certificate2(Convert.FromBase64String(data.Certificate));
            var provider = x509.PublicKey.Key as RSACryptoServiceProvider;

            buffer = Convert.FromBase64String(data.Base64String);
            var signature = Convert.FromBase64String(data.Signature);
            if (provider.VerifyData(buffer, new SHA256CryptoServiceProvider(), signature))
                result = new Dictionary<string, string>();
            else
                return result;

            result.Add("Subject", x509.Subject);
            result.Add("Issuer", x509.Issuer);
            result.Add("SerialNumber", x509.SerialNumber);

            return result;
        }

        public static IDictionary<string, string> VerifyData(Pkcs7 data, out byte[] buffer)
        {
            IDictionary<string, string> result = null;

            SignedCms signedCms = new SignedCms();
            signedCms.Decode(Convert.FromBase64String(data.Base64String));

            try
            {
                signedCms.CheckSignature(true);
                buffer = signedCms.ContentInfo.Content;
                result = new Dictionary<string, string>();
            }
            catch
            {
                buffer = null;
                return result;
            }

            foreach (SignerInfo signerInfo in signedCms.SignerInfos)
            {
                var x509 = signerInfo.Certificate;
                foreach(CryptographicAttributeObject attributeObject in signerInfo.SignedAttributes)
                {
                    AsnEncodedData[] array = new AsnEncodedData[1];
                    attributeObject.Values.CopyTo(array, 0);
                    if (attributeObject.Oid.Value.CompareTo("1.2.840.113549.1.9.25.3") == 0)
                        result.Add("Nonce", Encoding.UTF8.GetString(array[0].RawData, 2, array[0].RawData.Length - 2));
                    else if (attributeObject.Oid.Value.CompareTo("1.2.840.113549.1.9.5") == 0)
                    {
                        var s = Encoding.UTF8.GetString(array[0].RawData, 2, array[0].RawData.Length - 2);
                        result.Add("SignTime", DateTime.ParseExact(s, "yyMMddHHmmssZ", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).ToString("yyyy/MM/dd HH:mm:ss"));
                    }
                    else if (attributeObject.Oid.Value.CompareTo("2.16.886.1.100.2.204") == 0)
                        result.Add("CardNumber", Encoding.UTF8.GetString(array[0].RawData, 2, array[0].RawData.Length - 2));
                }

                result.Add("Subject", x509.Subject);
                result.Add("Issuer", x509.Issuer);
                result.Add("SerialNumber", x509.SerialNumber);

                break;
            }

            return result;
        }
    }
}
