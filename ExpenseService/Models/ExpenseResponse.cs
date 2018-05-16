using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace ExpenseService.Models
{
    public class ExpenseResponse
    {
        [JsonProperty("type")]
        public String Type { get; set; }
        [JsonProperty("amount")]
        public String Amount { get; set; }
        [JsonProperty("date")]
        public String Date { get; set; }
        [JsonProperty("reference_id")]
        public String ReferenceId { get; set; }
        [JsonProperty("company_id")]
        public String CompanyId { get; set; }

        public ExpenseResponse(Expense expense)
        {
            this.Type = expense.Type;
            this.Amount = expense.Amount.ToString();
            this.Date = expense.Date.ToShortDateString();
            this.ReferenceId = expense.ReferenceIdString;
            this.CompanyId = expense.CompanyId;
        }
    }
}