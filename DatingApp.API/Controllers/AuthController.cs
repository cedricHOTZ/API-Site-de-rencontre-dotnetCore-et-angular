using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo){
        _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password){

          username = username.ToLower();

          if( await _repo.UserExists(username))
          return BadRequest(" Le nom est déjà utilisé");

          var userToCreate = new User{
              UserName = username
          };

          var createdUser = await _repo.Register(userToCreate, password);
          return StatusCode(201);
        }
    }
}