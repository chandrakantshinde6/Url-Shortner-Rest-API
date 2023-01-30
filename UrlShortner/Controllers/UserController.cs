using Microsoft.AspNetCore.Mvc;

namespace UrlShortner.Controllers
{
    [ApiController]
    [Route("/api/v2/user/")]
    public class UserController : ControllerBase
    {

        [HttpGet]
        [Route("profile")]
        public JsonResult GetProfile()
        {
            return new JsonResult("Here is your profile");
        }
        
    }
}