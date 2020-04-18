using System.Threading.Tasks;
using DatingApp.API.Models;

namespace DatingApp.API.Data
{
    public interface IAuthRepository
    {
        //inscription
         Task<User> Register(User user,string password);
        //connection
         Task<User> Login(string username, string password );
        // est ce que l'username existe déjà
         Task<bool> UserExists(string username);
    }
}