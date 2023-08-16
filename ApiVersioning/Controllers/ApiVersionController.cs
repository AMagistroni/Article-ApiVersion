using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace ApiVersioning.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //Questa versione è deprecata ma ancora esistente. 
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    public class ApiVersionController : ControllerBase
    {
        //Questo metodo non ha versioni differenti. Ne esiste una sola
        [HttpGet()]
        [Route("MetodoSenzaVersione")]
        public IActionResult Get()
        {
            return Ok("Metodo senza versioni differenti");
        }

        //Può essere richiamato con il reader QueryString, MetodoGetConVersione?api-version=1.0
        //Può essere richiamato con il reader Header, inserendo nella chiamata, negli header api-version=1.0
        [HttpGet(), MapToApiVersion("1.0")]
        [Route("MetodoGetConVersione")]
        public IActionResult GetV1()
        {
            return Ok("Metodo in Get V1");
        }

        //Questo metodo ha la versione 2.0. Per cui, se lo chiamiamo senza dichiarare la versione,
        //dei due metodi con lo stesso nome verrà eseguito il primo, in quanto la versione di default in program.cs è 1.0
        [HttpGet(), MapToApiVersion("2.0")]
        [Route("MetodoGetConVersione")]
        public IActionResult GetV2()
        {
            return Ok("Metodo in Get V2");
        }
    }
}