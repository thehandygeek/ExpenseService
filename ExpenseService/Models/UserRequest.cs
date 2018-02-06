using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;

namespace ExpenseService.Models
{
    public class UserRequest
    {
        [JsonProperty("user_name")]
        public String UserName { get; set; }
        [JsonProperty("password")]
        public String Password { get; set; }
        [JsonProperty("user_type")]
        public String UserType { get; set; }
    }
}