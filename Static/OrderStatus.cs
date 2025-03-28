using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Static
{
    public static class OrderStatus
    {
        public static string Created = "CREATED";
        public static string Paying = "PAYING";
        public static string Preparing = "PREPARING";
        public static string Shipping = "SHIPPING";
        public static string Completed = "COMPLETED";
        public static string Canceling = "CANCELING";
        public static string Canceled = "CANCELED";
    }
}