using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;

namespace BlogPessoal.src.Services
{
    public interface IAuthentication
    { 
            string CodePassword(string password);
            void CreatedUserWithoutDuplicate(NewUserDTO user);
            string GeneretedToken(UserModel user);
            AuthorizationDTO GetAuthorization(AuthenticationDTO authentication);
        
    }
}

