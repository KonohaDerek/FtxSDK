using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTX.SDK.Response
{
    public class SpotMarginOfferResponse : ResponseBase<List<SpotMarginOfferDto>>
    {

    }
    public class SpotMarginOfferDto
    {
        public string coin { get; set; }
        public double size { get; set; }
        public double rate { get; set; }
    }
}
