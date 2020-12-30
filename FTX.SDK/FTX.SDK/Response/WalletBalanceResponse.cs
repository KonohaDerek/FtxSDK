using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTX.SDK.Response
{
    public class WalletBalanceResponse : ResponseBase<List<WalletBalanceDto>>
    {
        
    }
    public class WalletBalanceDto
    {
        public double availableWithoutBorrow { get; set; }
        public string coin { get; set; }
        public double free { get; set; }
        public double spotBorrow { get; set; }
        public double total { get; set; }
        public double usdValue { get; set; }
    }
}
