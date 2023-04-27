using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyRateApp.Models
{
    public class CurrencyConverter
    {

        [JsonProperty("r030")]
        public long NumberCode { get; set; }

        [JsonProperty("txt")]
        public string Name { get; set; }

        [JsonProperty("rate")]
        public decimal Rate { get; set; }

        [JsonProperty("cc")]
        public string LetterCode { get; set; }

        [JsonIgnore]
        public DateTime Exchangedate { get; set; }
    }

}
