using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace FTX.SDK.Tests
{
    [TestClass()]
    public class FtxClientTests
    {
        [TestMethod()]
        public void GetAccountTest()
        {
            var secret = Environment.GetEnvironmentVariable("FTX_SECRET");
            var key = Environment.GetEnvironmentVariable("FTX_KEY");
            var client = new FtxClient(secret, key);
            var response = client.GetAccount();
            Assert.IsFalse(string.IsNullOrWhiteSpace(response));
        }

        [TestMethod()]
        public void GetWalletBalanceTest()
        {
            var secret = Environment.GetEnvironmentVariable("FTX_SECRET");
            var key = Environment.GetEnvironmentVariable("FTX_KEY");
            var client = new FtxClient(secret, key);
            var response = client.GetWalletBalance();
            Assert.IsTrue(response.Any());
        }

        [TestMethod()]
        public void GetSpotMarginOffersTest()
        {
            var secret = Environment.GetEnvironmentVariable("FTX_SECRET");
            var key = Environment.GetEnvironmentVariable("FTX_KEY");
            var client = new FtxClient(secret, key);
            var response = client.GetSpotMarginOffers();
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Any());
        }

        [TestMethod()]
        public void GetSpotMarginLendingInfoTest()
        {
            var secret = Environment.GetEnvironmentVariable("FTX_SECRET");
            var key = Environment.GetEnvironmentVariable("FTX_KEY");
            var client = new FtxClient(secret, key);
            var response = client.GetSpotMarginLendingInfo();
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Any());
        }

        [TestMethod()]
        public void PostSpotMarginOffersTest()
        {
            var secret = Environment.GetEnvironmentVariable("FTX_SECRET");
            var key = Environment.GetEnvironmentVariable("FTX_KEY");
            var client = new FtxClient(secret, key);
            var response = client.PostSpotMarginOffers(new Request.PostSpotMarginOffersRequest() { 
                coin = "USD",
                rate = 9.13E-06F,
                size = 70.04098F
            });
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Any());
        }
    }
}