using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WsVenta.Models.Request;

namespace WsVenta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("login")]
        public IActionResult Autentificar([FromBody] AuthRequest model)
        {

            return Ok(model);

        }
    }
}
