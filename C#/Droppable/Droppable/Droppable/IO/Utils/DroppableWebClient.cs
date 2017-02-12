using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Droppable.IO.Utils
{
    public class DroppableWebClient : WebClient
    {
        public bool HeadOnly { get; set; }

        private string[] UserAgents = new string[]
        {
            // chrome
            "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; WOW64; Trident/5.0)"
        };

        public DroppableWebClient(bool headOnly = false)
            : base()
        {
            this.HeadOnly = headOnly;
            this.Headers.Add("user-agent", PickRandomUserAgent());
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var req = base.GetWebRequest(address);

            if (this.HeadOnly)
                req.Method = "HEAD";

            return req;
        }

        private string PickRandomUserAgent()
        {
            return this.UserAgents[new Random().Next(0, this.UserAgents.Length)];
        }
    }
}
