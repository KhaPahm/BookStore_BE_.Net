using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Interfaces
{
    public interface IPaypalService
    {
        Task<string> CreatePayment(decimal total, string returnUrl, string cancelUrl);
        Task<bool> CapturePayment(string orderId);
    }
}