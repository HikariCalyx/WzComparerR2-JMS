using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WzComparerR2.NetworkAI.Contracts
{
    [JsonObject("15")]
    public sealed class PackUserProfileUpdateReq
    {
        public string NickName { get; set; }
    }
}
