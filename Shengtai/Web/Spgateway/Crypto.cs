using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai.Web.Spgateway
{
    public class Crypto
    {
        private string key;
        private string iv;

        public Crypto(string key, string iv)
        {
            this.key = key;
            this.iv = iv;
        }

        private byte[] AddPKCS7Padding(byte[] src, int blockSize)
        {
            byte padding = (byte)(blockSize - (src.Length % blockSize));
            var dst = new byte[src.Length + padding];

            Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            for (int i = src.Length; i < dst.Length; i++)
                dst[i] = padding;

            return dst;
        }

        private byte[] RemovePKCS7Padding(byte[] src)
        {
            var dst = new byte[src.Length - src[src.Length - 1]];
            Buffer.BlockCopy(src, 0, dst, 0, dst.Length);
            return dst;
        }

        private string ByteArrayToHexString(byte[] bytes)
        {
            char[] c = new char[bytes.Length * 2];

            byte b;
            for (int i = 0; i < bytes.Length; ++i)
            {
                b = (byte)(bytes[i] >> 4);
                c[i * 2] = (char)(b > 9 ? b + 0x37 : b + 0x30);

                b = (byte)(bytes[i] & 0xF);
                c[i * 2 + 1] = (char)(b > 9 ? b + 0x37 : b + 0x30);
            }

            return new string(c);
        }

        public string GetTradeInfo(RequestTrade trade)
        {
            var properties = trade.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Select(x => new { x.Name, Value = x.GetValue(trade, null) })
                .Where(x => x.Value != null && !string.IsNullOrWhiteSpace(x.Value.ToString()))
                .ToDictionary(x => x.Name, x => x.Value);

            var s = string.Join("&", properties.Select(x => x.Key + "=" + x.Value));
            var inputBuffer = this.AddPKCS7Padding(Encoding.UTF8.GetBytes(s), 32);

            var aes = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(this.key),
                IV = Encoding.UTF8.GetBytes(this.iv),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.None
            };

            var bytes = aes.CreateEncryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            return this.ByteArrayToHexString(bytes).ToLower();
        }

        public string GetTradeSha(string tradeInfo)
        {
            var s = $"HashKey={this.key}&{tradeInfo}&HashIV={this.iv}";
            var buffer = Encoding.UTF8.GetBytes(s);

            var sha = new SHA256Managed();
            var hash = sha.ComputeHash(buffer);

            var builder = new StringBuilder();
            foreach (var b in hash)
                builder.AppendFormat("{0:x2}", b);

            return builder.ToString().ToUpper();
        }

        private byte[] HexStringToByteArray(string hexString)
        {
            byte[] bytes = new byte[hexString.Length / 2];
            for(int i = 0; i < hexString.Length; i += 2)
            {
                int topChar = (hexString[i] > 0x40 ? hexString[i] - 0x37 : hexString[i] - 0x30) << 4;
                int bottomChar = hexString[i + 1] > 0x40 ? hexString[i + 1] - 0x37 : hexString[i + 1] - 0x30;
                bytes[i / 2] = Convert.ToByte(topChar + bottomChar);
            }

            return bytes;
        }

        public ResponseTrade<ResponseResult> GetResult(string responseTradeInfo)
        {
            var inputBuffer = this.HexStringToByteArray(responseTradeInfo.ToUpper());
            var aes = new RijndaelManaged
            {
                Key = Encoding.UTF8.GetBytes(this.key),
                IV = Encoding.UTF8.GetBytes(this.iv),
                Mode = CipherMode.CBC,
                Padding = PaddingMode.None
            };

            var src = aes.CreateDecryptor().TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
            var dst = this.RemovePKCS7Padding(src);

            var value = Encoding.UTF8.GetString(dst);

            // todo: 解出字串已驗證ok, 剩物件轉換的實際測試
            var result = JsonConvert.DeserializeObject<ResponseTrade<ResponseResult>>(value);
            return result;
        }

        public ResponseTrade<ResponseResultCode> GetResultCode(string responseTradeInfo)
        {
            return null;
        }
    }
}
