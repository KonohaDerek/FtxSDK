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
        public float lendable { get; set; }
        public float minRate { get; set; }
        public float locked { get; set; }
        public float offered { get; set; }
    }
}
