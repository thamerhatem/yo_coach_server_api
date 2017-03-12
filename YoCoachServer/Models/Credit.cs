﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YoCoachServer.Models.Enums;

namespace YoCoachServer.Models
{
    public class Credit
    {
        public string Id { get; set; }
        [JsonProperty("credit_policy")]
        public CreditPolicy? CreditPolicy { get; set; }
        [JsonProperty("unit_value")]
        public double? UnitValue { get; set; }
        public double? Amount { get; set; }
        [JsonProperty("expires_at")]
        public string ExpiresAt { get; set; }
        [JsonProperty("day_of_payment")]
        public int? DayOfPayment { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}