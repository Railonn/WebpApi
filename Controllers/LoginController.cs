using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public LoginController(ApiDbContext context) 
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> LoginUser(UserData user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            
            if(existingUser == null)
                return NotFound();

            if(existingUser.Password == user.Password)
            {
                return Ok(existingUser.UserId);
            }
            return BadRequest("Refused");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> AskActivationUser(int id)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            
            if(existingUser == null)
                return NotFound();

            if(existingUser.ActivationDone)
            {
                return Ok(existingUser.ActivationDone);
            }
            return BadRequest(existingUser.ActivationDone);
        }
    }
}