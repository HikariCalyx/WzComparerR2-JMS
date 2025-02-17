using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WzComparerR2.NetworkAI.Contracts
{
    [JsonObject("4")]
    public sealed class PackLoginResp
    {
        public string SessionID { get; set; }
    }
}
