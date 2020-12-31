using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTX.SDK.Request
{
    public class PostSpotMarginOffersRequest
    {
        public string coin { get; set; }
        public float size { get; set; }

        public float rate { get; set; }


    }
}
