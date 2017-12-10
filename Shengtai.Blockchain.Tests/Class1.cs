using NBitcoin;
using NUnit.Framework;
using QBitNinja.Client;
using QBitNinja.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// https://github.com/MetacoSA/NBitcoin
namespace Shengtai.Blockchain.Tests
{
    [TestFixture]
    public class Class1
    {
        // https://programmingblockchain.gitbooks.io/programmingblockchain/content/bitcoin_transfer/bitcoin_address.html
        [Test]
        public void BitconinAddress()
        {
            Key priviteKey = new Key();
            PubKey publicKey = priviteKey.PubKey;

            Console.WriteLine(publicKey);
            Console.WriteLine(publicKey.GetAddress(Network.Main));
            Console.WriteLine(publicKey.GetAddress(Network.TestNet));

            var publicKeyHash = publicKey.Hash;
            Console.WriteLine(publicKeyHash);

            var mainNetAddress = publicKeyHash.GetAddress(Network.Main);
            var testNetAddress = publicKeyHash.GetAddress(Network.TestNet);
            Console.WriteLine(mainNetAddress);
            Console.WriteLine(testNetAddress);
        }

        // https://programmingblockchain.gitbooks.io/programmingblockchain/content/bitcoin_transfer/payment_script.html
        [Test]
        public void ScriptPubKey()
        {
            var publicKeyHash = new KeyId("14836dbe7f38c5ac3d49e8d790af808a4ee9edcf");
            var testNetAddress = publicKeyHash.GetAddress(Network.TestNet);
            var mainNetAddress = publicKeyHash.GetAddress(Network.Main);

            Console.WriteLine(mainNetAddress.ScriptPubKey);
            Console.WriteLine(testNetAddress.ScriptPubKey);

            var paymentScript = publicKeyHash.ScriptPubKey;
            var sameMainNetAddress = paymentScript.GetDestinationAddress(Network.Main);
            //Assert.AreEqual(mainNetAddress, sameMainNetAddress);

            var samePublicKeyHash = paymentScript.GetDestination() as KeyId;
            //Assert.AreEqual(publicKeyHash, samePublicKeyHash);

            var sameMainNetAddress2 = new BitcoinPubKeyAddress(samePublicKeyHash, Network.Main);
            Assert.AreEqual(mainNetAddress, sameMainNetAddress2);
        }

        // https://programmingblockchain.gitbooks.io/programmingblockchain/content/bitcoin_transfer/private_key.html
        [Test]
        public void PrivateKey()
        {
            Key privateKey = new Key();
            BitcoinSecret mainNetPrivateKey = privateKey.GetBitcoinSecret(Network.Main);
            BitcoinSecret testNetPrivateKey = privateKey.GetBitcoinSecret(Network.TestNet);
            Console.WriteLine(mainNetPrivateKey);
            Console.WriteLine(testNetPrivateKey);

            bool wifIsBitcoinSecret = mainNetPrivateKey == privateKey.GetWif(Network.Main);
            Assert.IsTrue(wifIsBitcoinSecret);

            PubKey publicKey = privateKey.PubKey;
            BitcoinPubKeyAddress bitcoinPublicKey = publicKey.GetAddress(Network.Main);
        }

        // https://programmingblockchain.gitbooks.io/programmingblockchain/content/bitcoin_transfer/transaction.html
        [Test]
        public void Transaction()
        {
            QBitNinjaClient client = new QBitNinjaClient(Network.Main);
            var transactionId = uint256.Parse("f13dc48fb035bbf0a6e989a26b3ecb57b84f85e0836e777d6edf60d87a4a2d94");
            GetTransactionResponse transactionResponse = client.GetTransaction(transactionId).Result;
            Transaction transaction = transactionResponse.Transaction;
            Console.WriteLine(transactionResponse.TransactionId);
            Console.WriteLine(transaction.GetHash());
            Console.WriteLine();

            List<ICoin> receivedCoins = transactionResponse.ReceivedCoins;
            foreach(var coin in receivedCoins)
            {
                Money amount = coin.Amount as Money;

                Console.WriteLine(amount.ToDecimal(MoneyUnit.BTC));
                var paymentScript = coin.TxOut.ScriptPubKey;
                Console.WriteLine(paymentScript);
                var address = paymentScript.GetDestinationAddress(Network.Main);
                Console.WriteLine(address);
                Console.WriteLine();
            }
            Console.WriteLine();

            var outputs = transaction.Outputs;
            foreach(TxOut output in outputs)
            {
                Money amount = output.Value;

                Console.WriteLine(amount.ToDecimal(MoneyUnit.BTC));
                var paymentScript = output.ScriptPubKey;
                Console.WriteLine(paymentScript);
                var address = paymentScript.GetDestinationAddress(Network.Main);
                Console.WriteLine(address);
                Console.WriteLine();
            }

            var inputs = transaction.Inputs;
            foreach (TxIn input in inputs)
            {
                OutPoint previousOutpoint = input.PrevOut;
                Console.WriteLine(previousOutpoint.Hash);
                Console.WriteLine(previousOutpoint.N);
                Console.WriteLine();
            }
        }
    }
}
