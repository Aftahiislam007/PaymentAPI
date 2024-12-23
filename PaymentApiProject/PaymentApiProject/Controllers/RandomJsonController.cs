using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentApiProject.Model;

namespace PaymentApiProject.Controllers
{
    [Route("api/random-json")]
    [ApiController]
    public class RandomJsonController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRandomJson()
        {
            var model = new SubscriptionModel
            {
                Amount = "1",
                FirstPaymentIncludedInCycle = "True",
                ServiceId = "100001",
                Currency = "BDT",
                StartDate = DateTime.Now.ToString("yyyy-MM-dd"),
                ExpiryDate = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd"),
                Frequency = "WEEKLY",
                SubscriptionType = "BASIC",
                MaxCapRequired = "False",
                MerchantShortCode = "01307153119",
                PayerType = "CUSTOMER",
                PaymentType = "FIXED",
                RedirectUrl = "http://hello.com",
                SubscriptionRequestId = Guid.NewGuid().ToString(),
                SubscriptionReference = "01797341164",
                CKey = "000001"
            };
            return Ok(model);
        }
    }
}
