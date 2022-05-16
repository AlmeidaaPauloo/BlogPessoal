using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using System.Threading.Tasks;

namespace BlogPessoal.src.Services
{
    public interface IAuthentication
    { 
            string CodePassword(string password);
            Task CreatedUserWithoutDuplicateAsync(NewUserDTO user);
            string GeneretedToken(UserModel user);
            Task<AuthorizationDTO> GetAuthorizationAsync(AuthenticationDTO authentication);
        
    }
}

