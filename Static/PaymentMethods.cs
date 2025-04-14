using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Static
{
    public static class PaymentMethods
    {
        public static string Paypal { get; set; } = "PAYPAL";
        public static string Cash { get; set; } = "CASH";
    }
}