using Microsoft.VisualStudio.TestTools.UnitTesting;
using FTX.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTX.SDK.Tests
{
    [TestClass()]
    public class FtxClientTests
    {
        [TestMethod()]
        public void GetAccountTest()
        {
            var secret = "Iuq-P-Fh1LDumDP92omR22-LJqthR__VTeBak64M";
            var key = "wZRUBZO3TQ4Kq7svl_Qgp_UqjGo_DUOJdiN2BZZO";
            var client = new FtxClient(secret, key);
            var response = client.GetAccount();
            Assert.IsFalse(string.IsNullOrWhiteSpace(response));
        }

        [TestMethod()]
        public void GetWalletBalanceTest()
        {
            var secret = "Iuq-P-Fh1LDumDP92omR22-LJqthR__VTeBak64M";
            var key = "wZRUBZO3TQ4Kq7svl_Qgp_UqjGo_DUOJdiN2BZZO";
            var client = new FtxClient(secret, key);
            var response = client.GetWalletBalance();
            Assert.IsTrue(response.Any());
        }

        [TestMethod()]
        public void GetSpotMarginOffersTest()
        {
            var secret = "Iuq-P-Fh1LDumDP92omR22-LJqthR__VTeBak64M";
            var key = "wZRUBZO3TQ4Kq7svl_Qgp_UqjGo_DUOJdiN2BZZO";
            var client = new FtxClient(secret, key);
            var response = client.GetSpotMarginOffers();
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Any());
        }

        [TestMethod()]
        public void GetSpotMarginLendingInfoTest()
        {
            var secret = "Iuq-P-Fh1LDumDP92omR22-LJqthR__VTeBak64M";
            var key = "wZRUBZO3TQ4Kq7svl_Qgp_UqjGo_DUOJdiN2BZZO";
            var client = new FtxClient(secret, key);
            var response = client.GetSpotMarginLendingInfo();
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Any());
        }

        [TestMethod()]
        public void PostSpotMarginOffersTest()
        {
            var secret = "Iuq-P-Fh1LDumDP92omR22-LJqthR__VTeBak64M";
            var key = "wZRUBZO3TQ4Kq7svl_Qgp_UqjGo_DUOJdiN2BZZO";
            var client = new FtxClient(secret, key);
            var response = client.PostSpotMarginOffers(new Request.PostSpotMarginOffersRequest() { 
                coin = "USD",
                rate = 9.13E-06,
                size = 70.04098

            });
            Assert.IsNotNull(response);
            Assert.IsTrue(response.Any());
        }
    }
}