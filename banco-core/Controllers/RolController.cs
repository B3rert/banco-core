using banco_core.Modelo;
using banco_core.Models;
using banco_core.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace banco_core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController(BancoContext context) : ControllerBase
    {
        private readonly BancoContext _context = context;

        [HttpGet()]
        public async Task<IActionResult> ObtenerRoles()
        {
            try
            {
                var roles = await _context.Rol.ToListAsync();
                if (roles == null || roles.Count == 0)
                {
                    return Ok(new RespondeModel()
                    {
                        Data = new List<RolModel>(),
                        Success = true,
                    });
                }

                return Ok(new RespondeModel()
                {
                    Success = true,
                    Data = roles,
                });
            }
            catch (Exception e)
            {
                return BadRequest(new RespondeModel()
                {
                    Data = e.Message,
                    Success = false,
                });
            }
        }


    }
}
