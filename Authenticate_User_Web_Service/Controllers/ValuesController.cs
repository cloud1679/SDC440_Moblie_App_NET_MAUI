using Microsoft.AspNetCore.Mvc;

namespace Authenticate_User_Web_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET: api/Values
        [HttpGet]
        [BasicAuthentication] // Apply the BasicAuthentication filter to this action.
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
