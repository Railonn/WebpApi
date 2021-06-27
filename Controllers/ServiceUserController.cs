using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/service/user/")]
    public class ServiceUserController : ControllerBase
    {

        private readonly ApiDbContext _context;

        public ServiceUserController(ApiDbContext context) 
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getUserServiceName(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

            if(user == null)
                return NotFound();

            var userService = await _context.UsersServices.FirstOrDefaultAsync(x => x.ServiceId == user.UserServiceId);

            if(userService == null)
                return NotFound();

            return Ok(userService.ServiceName);
        }
    }
}