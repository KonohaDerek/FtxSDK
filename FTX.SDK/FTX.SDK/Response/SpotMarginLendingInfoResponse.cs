using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTX.SDK.Response
{
    public class SpotMarginLendingInfoResponse : ResponseBase<List<SpotMarginLendingInfoDto>>
    {

    }
    public class SpotMarginLendingInfoDto
    {
        public string coin { get; set; }
        public double lendable { get; set; }
        public double minRate { get; set; }
        public double locked { get; set; }
        public double offered { get; set; }
    }
}
