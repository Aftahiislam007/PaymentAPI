using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApiProject.Model;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace PaymentApiProject.Controllers
{
    [Route("api/checkout")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        /*private object JsonConvert;*/

        public CheckoutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [HttpPost]
        public async Task<IActionResult> GeneratePaymentUrl([FromQuery] string frequency)
        {
            if (frequency != "WEEKLY" && frequency != "MONTHLY")
            {
                return BadRequest("Invalid frequency. Allowed values: WEEKLY, MONTHLY");
            }
                

            var client = _httpClientFactory.CreateClient();
            var subscriptionRequestId = Guid.NewGuid().ToString();
            var model = new SubscriptionModel
            {
                Amount = "1",
                FirstPaymentIncludedInCycle = "True",
                ServiceId = "100001",
                Currency = "BDT",
                StartDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ExpiryDate = frequency == "WEEKLY"
                    ? DateTime.Now.AddDays(7).ToString("yyyy-MM-dd")
                    : DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"),
                Frequency = frequency,
                SubscriptionType = "BASIC",
                MaxCapRequired = "False",
                MerchantShortCode = "01307153119",
                PayerType = "CUSTOMER",
                PaymentType = "FIXED",
                RedirectUrl = "http://hello1.com",
                SubscriptionRequestId = subscriptionRequestId,
                SubscriptionReference = "01797341164",
                CKey = "000001"
            };

            var request = new HttpRequestMessage(HttpMethod.Post, "https://bkashtest.shabox.mobi/home/MultiTournamentInBuildCheckoutUrl")
            {
                Headers = { { "api-key", "796b8b9dbbf46b1d8fd73f68979ae31635da9afabc9dee147adf0440ee7118a8" } },
                Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json")
            };

            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
                return BadRequest("Failed to generate payment URL.");

            var result = await response.Content.ReadAsStringAsync();
            return Ok(new
            {
                PaymentUrl = result,
                SubscriptionRequestId = subscriptionRequestId
            }); 
        }

    }
}
