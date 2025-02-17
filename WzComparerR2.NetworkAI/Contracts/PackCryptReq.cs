using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WzComparerR2.NetworkAI.Contracts
{
    [JsonObject("1")]
    public sealed class PackCryptReq
    {
        public byte[] Exponent { get; set; }
        public byte[] Modulus { get; set; }
    }
}
