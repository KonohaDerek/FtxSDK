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
        public float availableWithoutBorrow { get; set; }
        public string coin { get; set; }
        public float free { get; set; }
        public float spotBorrow { get; set; }
        public float total { get; set; }
        public float usdValue { get; set; }
    }
}
