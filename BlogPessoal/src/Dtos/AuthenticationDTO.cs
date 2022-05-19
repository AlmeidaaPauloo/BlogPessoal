using BlogPessoal.src.utilities;
using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.Dtos
{

    public class AuthenticationDTO
    {
        /// <summary>
        /// <para>Resume> Created AuthenticationDTO</para>
        /// <para>Criado por: Paulo Almeida</para>
        /// <para>Version: 1.0</para>
        /// <para>Data: 10/05/2022</para>
        /// </summary>
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public AuthenticationDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
    public class AuthorizationDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public UserType Type { get; set; }
        public string Token { get; set; }
        public AuthorizationDTO(int id, string email, UserType type, string token)
        {
            Id = id;
            Email = email;
            Type = type;
            Token = token;
        }
    }
}
    
