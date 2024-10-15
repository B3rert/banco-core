using banco_core.Modelo;
using banco_core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace banco_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController(BancoContext context) : ControllerBase
    {
        private readonly BancoContext _context = context;

        //[HttpGet]
        //public async Task<IActionResult> GetRol()
        //{


        //}

    }
}
