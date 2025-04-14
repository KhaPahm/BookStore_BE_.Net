using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BookStore.Interfaces;
using BookStore.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace BookStore.Services
{
    public class PaypalService : IPaypalService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<PaypalSettings> _config;

        public PaypalService(IHttpClientFactory httpClientFactory, IOptions<PaypalSettings> config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        public async Task<string> GetAccessTokenAsync() {
            var client = _httpClientFactory.CreateClient();
            var byteArr = Encoding.ASCII.GetBytes($"{_config.Value.ClientId}:{_config.Value.ClientSecret}");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArr));

            var body = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
            var response = await client.PostAsync("https://api-m.sandbox.paypal.com/v1/oauth2/token", body);
            var json = await response.Content.ReadAsStringAsync();

            dynamic? data = JsonConvert.DeserializeObject(json);

            return data == null ? "" : data.access_token;
        }

        public async Task<bool> CapturePayment(string orderId)
        {
            var accessToken = await GetAccessTokenAsync();
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var content = new StringContent(JsonConvert.SerializeObject(null), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"https://api-m.sandbox.paypal.com/v2/checkout/orders/{orderId}/capture", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> CreatePayment(decimal total, string returnUrl, string cancelUrl)
        {
            var accessToken = await GetAccessTokenAsync();
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var request = new {
                intent = "CAPTURE",
                purchase_units = new[] {
                    new {
                        amount = new {
                            currency_code = "USD",
                            value = total.ToString("F2")
                        }
                    }
                },
                    application_context = new {
                    return_url = returnUrl,
                    cancel_url = cancelUrl
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api-m.sandbox.paypal.com/v2/checkout/orders", content);
            var result = await response.Content.ReadAsStringAsync();
            dynamic order = JsonConvert.DeserializeObject(result);
            IEnumerable<dynamic> links = order.links;
            var lstLink = links.ToList();
            var approvalLink = lstLink.First(link => link.rel == "approve").href;

            return approvalLink;
        }
    }
}