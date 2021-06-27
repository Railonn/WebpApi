using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UserController(ApiDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers() 
        {
            var users = await _context.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromBody] UserData user)
        {
            if(ModelState.IsValid) 
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);

                if(existingUser != null) {
                    return new JsonResult("This email address is already in use.") {StatusCode = 500};
                }

                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                
                return CreatedAtAction("GetUser", new {user.UserId}, user);
            }

            return BadRequest("Something went wrong.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id) 
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.UserId == id);

            if(user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserData user)
        {
            if(id != user.UserId) 
                return BadRequest();

            var existUser = await _context.Users.FirstOrDefaultAsync(user => user.UserId == id);

            if(existUser == null)
                return NotFound();

            existUser.Name = user.Name;
            existUser.LastName = user.LastName;
            existUser.PhoneNumber = user.PhoneNumber;
            existUser.Adress = user.Adress;
            existUser.City = user.City;
            existUser.Country = user.Country;
            existUser.ActivationDone = user.ActivationDone;

            // Implement the changes on the database level
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteUser(int id)
        {
            var existUser = await _context.Users.FirstOrDefaultAsync(user => user.UserId == id);

            if(existUser == null) 
                return NotFound();

            _context.Users.Remove(existUser);
            await _context.SaveChangesAsync();

            return Ok(existUser);
        }
    }
}