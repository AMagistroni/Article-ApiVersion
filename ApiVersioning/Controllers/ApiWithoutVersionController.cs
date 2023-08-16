using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersioning.Controllers
{
    [ApiController]
    [ApiVersionNeutral]
    [Route("api/[controller]")]
    public class ApiWithoutVersionController : ControllerBase
    {
        [HttpGet()]
        [Route("Get")]
        public IActionResult Get()
        {
            return Ok("Metodo senza versioni");
        }
    }
}
