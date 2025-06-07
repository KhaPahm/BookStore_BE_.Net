using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Static
{
    public static class PaypalExcute
    {
        public static string Success = "http://localhost:5075/payment-success";
        public static string Cancel = "http://localhost:5075/payment-cancel";
    }
}