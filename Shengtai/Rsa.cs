using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Shengtai
{
    public class Rsa
    {
        private readonly CspParameters parameters;
        private RSACryptoServiceProvider provider = null;
        public Rsa(string keyContainerName)
        {
            this.parameters = new CspParameters(1024)
            {
                KeyContainerName = keyContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore,
                ProviderName = "Microsoft Strong Cryptographic Provider",
                ProviderType = 1
            };
            this.SetProvider();
        }

        private void SetProvider(bool regenerate = true)
        {
            if (regenerate)
            {
                if (this.provider != null)
                {
                    this.provider.PersistKeyInCsp = false;
                    this.provider.Clear();
                }

                this.provider = new RSACryptoServiceProvider(this.parameters);
            }
        }

        public void GenerateKey(Action<string> setPublicKey, Action<string> setPrivateKey, bool regenerate = true)
        {
            this.SetProvider(regenerate);

            setPublicKey(Convert.ToBase64String(Encoding.UTF8.GetBytes(this.provider.ToXmlString(false))));
            setPrivateKey(Convert.ToBase64String(Encoding.UTF8.GetBytes(this.provider.ToXmlString(true))));
        }

        public string Encrypt(string s, string publicKey)
        {
            this.SetProvider(this.provider == null);
            this.provider.FromXmlString(Encoding.UTF8.GetString(Convert.FromBase64String(publicKey)));

            return Convert.ToBase64String(this.provider.Encrypt(Encoding.UTF8.GetBytes(s), false));
        }

        public string Decrypt(string s, string privateKey)
        {
            this.SetProvider(this.provider == null);
            this.provider.FromXmlString(Encoding.UTF8.GetString(Convert.FromBase64String(privateKey)));

            return Encoding.UTF8.GetString(this.provider.Decrypt(Convert.FromBase64String(s), false));
        }

        public string SignData(string s, string privateKey)
        {
            this.SetProvider(this.provider == null);
            this.provider.FromXmlString(Encoding.UTF8.GetString(Convert.FromBase64String(privateKey)));

            return Convert.ToBase64String(this.provider.SignData(Encoding.UTF8.GetBytes(s), new SHA1CryptoServiceProvider()));
        }

        public bool VerifyData(string s, string privateKey, string publicKey, string signature, out string data)
        {
            this.SetProvider(this.provider == null);
            this.provider.FromXmlString(Encoding.UTF8.GetString(Convert.FromBase64String(publicKey)));

            data = this.Decrypt(s, privateKey);
            return this.provider.VerifyData(Encoding.UTF8.GetBytes(data), new SHA1CryptoServiceProvider(),
                Convert.FromBase64String(signature));
        }
    }
}
