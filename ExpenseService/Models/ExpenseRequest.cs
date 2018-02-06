using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace ExpenseService.Models
{
    public class ExpenseRequest
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("amount")]
        public string Amount { get; set; }
        [JsonProperty("date")]
        public string Date { get; set; }
    }
}