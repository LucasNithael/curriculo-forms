using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected IActionResult CustomResponse(object? result, int statusCode)
        {
            return StatusCode(statusCode, new
            {
                success = statusCode >= 200 && statusCode < 300,
                data = result
            });
        }
    }
}
