using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WordGameAPI
{
    public class WordModel
    {
        [JsonPropertyName("entry")]
        public string Entry { get; set; }

        [JsonPropertyName("result_msg")]
        public string ResultMessage { get; set; }

        [JsonPropertyName("result_code")]
        public string ResultCode { get; set; }
    }
}
