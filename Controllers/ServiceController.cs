using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ServiceController(ApiDbContext context) 
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getServiceName(int id)
        {
            var userService = await _context.UsersServices.FirstOrDefaultAsync(x => x.ServiceId == id);

            if(userService == null)
                return NotFound();

            return Ok(userService.ServiceName);
        }
    }
}