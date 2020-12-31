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
            if (args.Length == 0)
            {
                Console.WriteLine($"Use Default Currency : {currency}");
            }
            else
            {
                currency = args[0];
                Console.WriteLine($"Run With Currency : {currency}");
            }

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

            var client = new FtxClient(secret, key);
            var infos = client.GetSpotMarginLendingInfo();
            // select usd info
            var usd_info = infos.Where(o => o.coin == currency.ToUpper()).FirstOrDefault();
            if(usd_info != null)
            {
                var source = (float)Math.Floor(usd_info.offered * 1000000) / 1000000;
                var leadableUsd = (float)Math.Floor(usd_info.lendable * 1000000) / 1000000;
                var rate = usd_info.minRate;
                var coin = usd_info.coin;
                var retry_times = 0;
                do
                {
                    try
                    {
                        if (leadableUsd < usd_info.locked )
                        {
                            Console.WriteLine($"{DateTime.Now}: Update USD Spot Margin Leading Faild New LeadableUSD Less Than Now LeadableUSD");
                            return;
                        }
                        if ( retry_times > 5)
                        {
                            Console.WriteLine($"{DateTime.Now}: Update USD Spot Margin Leading Faild Over Retry Times");
                            return;
                        }
                        var response = client.PostSpotMarginOffers(new PostSpotMarginOffersRequest()
                        {
                            coin = coin,
                            rate = rate,
                            size = leadableUsd
                        });
                        Console.WriteLine($"{DateTime.Now}: Update USD Spot Margin Leading Success");
                        break;
                    }
                    catch (Exception ex)
                    {
                        leadableUsd -= 0.00001F;
                        Console.WriteLine($"{DateTime.Now}: Update USD Spot Margin Leading Faild , {ex.Message}");
                        Console.WriteLine($"{DateTime.Now}: Change leadableUsd to {leadableUsd}");
                    }
                    retry_times++;
                } while (true);
            }
            else
            {
                Console.WriteLine($"{DateTime.Now}: Can't Find USD Spot Margin Leading");
            }
            Console.WriteLine($"{DateTime.Now} :  Complete Update Spot Margin Leading");



        }
    }
}
