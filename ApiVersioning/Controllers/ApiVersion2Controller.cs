using ApiVersioning.BusinessLogic;
using ApiVersioning.V1.Models;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersioning.V1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [Route("api/VersionAllMethodsController")]
    public class ApiVersion2Controller : ControllerBase
    {
        private readonly IApiVersion2Business _apiVersion2Business;
        public ApiVersion2Controller(IApiVersion2Business apiVersion2Business)
        {
            _apiVersion2Business = apiVersion2Business;
        }
        [HttpPost()]
        [Route("Post")]
        public IActionResult Post(PostRequest postRequest)
        {
            return Ok($"Nome: {postRequest.Nome}");
        }

        [HttpPost()]
        [Route("PostUgualeFraVersioni")]
        public IActionResult PostUgualeFraVersioni(PostUgualeFraVersioniRequest postRequest)
        {
            return Ok(_apiVersion2Business.PostUgualeFraVersioni(postRequest));
        }
    }
}

namespace ApiVersioning.V2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("2.1-alfa")]
    [Route("api/v{version:apiVersion}/VersionAllMethodsController")]
    public class ApiVersion2Controller : ControllerBase
    {
        private readonly IApiVersion2Business _apiVersion2Business;
        public ApiVersion2Controller(IApiVersion2Business apiVersion2Business)
        {
            _apiVersion2Business = apiVersion2Business;
        }

        [HttpPost()]
        [Route("Post")]
        public IActionResult Post(Models.PostRequest postRequest)
        {
            return Ok($"Nome: {postRequest.Nome}\nCognome {postRequest.Cognome}");
        }

        [HttpPost()]
        [Route("PostUgualeFraVersioni")]
        public IActionResult PostUgualeFraVersioni(PostUgualeFraVersioniRequest postRequest)
        {
            return Ok(_apiVersion2Business.PostUgualeFraVersioni(postRequest));
        }
    }
}