using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WConfigurator.Data
{
    public class InfraRequest : AppRequest
    {
        public InfraRequest(string command)
        {
            Type = "req"; // "Request" by default
            Application = "ir";
            Command = command;
        }
    }
    public class InfraRequestValue : InfraRequest
    {
        public InfraRequestValue(string command, string value) : base(command)
        {
            Value = value;
        }

        [JsonProperty("v")]
        public string Value { get; set; }
    }

    public class ResponseButtonsList : AppRequest
    {
        [JsonProperty("v")]
        public string[] Items { get; set; }
    }
    public class ResponseButtonEdit : AppRequest
    {
        [JsonProperty("v")]
        public string ButtonName { get; set; }

        [JsonProperty("m")]
        public InfraButtonMode Mode { get; set; }

        [JsonProperty("hex", NullValueHandling = NullValueHandling.Ignore)]
        public string HexCode { get; set; }

        [JsonProperty("raw", NullValueHandling = NullValueHandling.Ignore)]
        public short[] Raw { get; set; }

        [JsonProperty("rn", NullValueHandling = NullValueHandling.Ignore)]
        public string NewButtonName { get; set; }

        [JsonIgnore]
        public string RawCode
        {
            get
            {
                if (Raw == null)
                    Raw = new short[0];
                return JsonConvert.SerializeObject(Raw);
            }
            set
            {
                var val = value.Trim();
                if (val.StartsWith('{') || val.EndsWith('}') || val.EndsWith(';'))
                    val = val.Trim("{};".ToCharArray());
                if (!val.StartsWith('[') || !val.EndsWith(']'))
                    val = "[" + val + "]";

                Raw = JsonConvert.DeserializeObject<short[]>(val);
            }
        }
    }

    public enum InfraButtonMode
    {
        Disabled = 0,
        RC5 = 1,
        RC6 = 2,
        NEC = 3,
        SONY = 4,
        SAMSUNG = 7,
        RAW = 30,
    }
}
