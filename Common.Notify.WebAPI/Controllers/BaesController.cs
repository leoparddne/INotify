using Common.Notify.WebAPI.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Common.Notify.WebAPI.Controllers
{
    [Route("api/notify/[controller]/[action]")]
    [ApiController]
    [APIResultFilter]
    public class BaesController : ControllerBase
    {
    }
}
