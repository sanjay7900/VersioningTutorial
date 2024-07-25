using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace VersioningTutorial.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("2")]
    [ApiExplorerSettings(GroupName ="v2")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult GetAction(int id)
        {
            return Ok();
        }
    }
}
