﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YoCoachServer.Models.Enums
{
    public enum StatePayment
    {
        PENDING,
        PAYED
    }

    public enum StateSchedule
    {
        SCHEDULED,
        CANCELED,
        MISSED
    }

    public enum CreditPolicy
    {
        PRE,
        POST
    }

    public enum ClientType
    {
        MONTHLY,
        LOOSE
    }
}