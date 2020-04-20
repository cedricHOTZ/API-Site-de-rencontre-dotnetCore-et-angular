using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;

        public readonly IConfiguration _config; 

        public AuthController(IAuthRepository repo, IConfiguration config){
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto){
         
        // Vérifie si le nom et le mot de passe est valid
        if(!ModelState.IsValid){
        return BadRequest(ModelState);
        }

          userForRegisterDto.Username = userForRegisterDto.Username.ToLower();

          if( await _repo.UserExists(userForRegisterDto.Username))
          return BadRequest(" Le nom est déjà utilisé");

          var userToCreate = new User
          {
              Username = userForRegisterDto.Username
          };

          var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);
          return StatusCode(201); 
        }
    
    [HttpPost("login")]

    public async Task<IActionResult> Login(UserForLoginDto userForLoginDto){
   //On s'enregistre pour nous assurer que nous avons un utilisateur
    var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);
   
   // Si l'utilisateur na pas le bon mot de passe ou nom correspondant aux valeurs stockées dans la BDD il r'envoie une erreur sans donné plus d'info
    if(userFromRepo == null)
    return Unauthorized();
    

    // Construction du jeton de connection
    var claims = new[]
    {
        new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),
        new Claim(ClaimTypes.Name, userFromRepo.Username)
    };
    //Clé non lisible (crypté)
    
    // Le serveur vérifie et  signe les jetons 
    var key = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(_config.GetSection("AppSettings:Token").Value));
   
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
    
    //Création du jeton
    var tokenDescriptor = new SecurityTokenDescriptor{
        Subject = new ClaimsIdentity(claims),
        //Date d'expiration de 24H
        Expires = DateTime.Now.AddDays(1),
        SigningCredentials = creds
         };

         var tokenHandler = new JwtSecurityTokenHandler();

         var token = tokenHandler.CreateToken(tokenDescriptor);

         return Ok(new{
             token = tokenHandler.WriteToken(token)
         });


    }
    }
}