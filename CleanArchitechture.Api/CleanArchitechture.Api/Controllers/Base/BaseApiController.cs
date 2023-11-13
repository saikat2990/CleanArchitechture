using CleanArchitechture.Core.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitechture.Api.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected ActionResult<ApiResult<T>> OkResult<T>(T data)
        {
            var result = new ApiResult<T>(data);
            return Ok(result);
        }
    }
}
