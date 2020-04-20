using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegisterDto
    {
          [Required]
        public string Username {get; set;}

          [Required]
          [MinLength(8,ErrorMessage="Le mot de passe doit faire au moins 8 caractères")]
         
        public string Password {get; set;}
    }
}