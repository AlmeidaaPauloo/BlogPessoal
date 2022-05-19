using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using System.Threading.Tasks;

namespace BlogPessoal.src.Services
{
    /// <summary>
    /// <para>Resume: Interface Responsible for represent action of authentication</para>
    /// <para>Created by: Paulo Almeida</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 13/05/2022</para>
    /// </summary>
    public interface IAuthentication
    { 
            string CodePassword(string password);
            Task CreatedUserWithoutDuplicateAsync(NewUserDTO user);
            string GeneretedToken(UserModel user);
            Task<AuthorizationDTO> GetAuthorizationAsync(AuthenticationDTO authentication);
        
    }
}

