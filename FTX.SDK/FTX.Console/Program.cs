using FTX.SDK;
using FTX.SDK.Request;
using System;
using System.Linq;

namespace FTXConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var currency = "USD";
            var subAccount = "";
            if (args.Length > 0)
            {
                currency = args[0];
            }

            if (args.Length > 1)
            {
                subAccount = args[1];
            }
           
            Console.WriteLine($"Run With Currency : {currency}");

            Console.WriteLine($"{DateTime.Now} :  Start Run Update Spot Margin Leading");
            var secret = Environment.GetEnvironmentVariable("FTX_SECRET");
            var key = Environment.GetEnvironmentVariable("FTX_KEY");
            if (string.IsNullOrWhiteSpace(secret))
            {
                Console.WriteLine($"{DateTime.Now}: Can't Find `FTX_SECRET` Environment Variable");
                return;
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                Console.WriteLine($"{DateTime.Now}: Can't Find `FTX_KEY` Environment Variable");
                return;
            }
            currency = currency.ToUpper();
            var client = new FtxClient(secret, key , subAccount);
            var infos = client.GetSpotMarginLendingInfo();
            // select usd info
            var usd_info = infos.Where(o => o.coin == currency).FirstOrDefault();
            if (usd_info == null)
            {
                Console.WriteLine($"{DateTime.Now}: Can't Find `{currency}` Currency");
                return;
            }

            var source = (float)Math.Floor(usd_info.offered * 1000000) / 1000000;
            var leadableUsd = (float)Math.Floor(usd_info.lendable * 1000000) / 1000000;
            var rate = usd_info.minRate;
            var coin = usd_info.coin;
            
            try
            {
                if (leadableUsd < usd_info.locked )
                {
                    Console.WriteLine($"{DateTime.Now}:New Leadable {leadableUsd}({currency})");
                    Console.WriteLine($"{DateTime.Now}:Now Leadable {usd_info.locked}({currency})");
                    Console.WriteLine($"{DateTime.Now}: Update USD Spot Margin Leading Faild New Leadable Less Than Now Leadable");
                    return;
                }
                
                var response = client.PostSpotMarginOffers(new PostSpotMarginOffersRequest()
                {
                    coin = coin,
                    rate = rate,
                    size = leadableUsd
                });
                Console.WriteLine($"{DateTime.Now}: Update {currency} Spot Margin Leading With {leadableUsd} Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{DateTime.Now}: Update {currency} Spot Margin Leading Faild , {ex.Message}");
                Console.WriteLine($"{leadableUsd}({currency})");
                leadableUsd -= 0.0001F;
                Console.WriteLine($"{leadableUsd}({currency})");
                Console.WriteLine($"{DateTime.Now}: Change leadable to {leadableUsd}({currency})");
            }
            Console.WriteLine($"{DateTime.Now} :  Complete Update Spot Margin Leading");
        }
    }
}
