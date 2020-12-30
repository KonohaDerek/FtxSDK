using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FTX.SDK.Response
{
    public class ResponseBase<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public List<T> result { get; set; }
        
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }

       
        public string error { get; set; }


    }
}
