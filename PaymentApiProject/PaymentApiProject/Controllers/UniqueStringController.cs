using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PaymentApiProject.Controllers
{
    [Route("api/unique-string")]
    [ApiController]
    public class UniqueStringController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUniqueString()
        {
            string uniqueString = Guid.NewGuid().ToString();
            return Ok(uniqueString);
        }
    }
}
