using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.Repositorys;
using BlogPessoal.src.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogPessoal.src.services.Implementations
{
    /// <summary>
    /// <para>Resume: Class responsible for implementing IAuthentication</para>
    /// <para>Created by: Paulo Almeida</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    public class AuthenticationServices : IAuthentication
    {
        #region Attributes
        private readonly IUser _repository;
        public AuthenticationServices(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        #endregion Attributes

        #region Constructors
        public AuthenticationServices(IUser repository, IConfiguration configuration)
        {
            _repository = repository;
            Configuration = configuration;
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// <para>Resumo: Method responsible for code password</para>
        /// </summary>
        /// <param name="password">Password to be coded</param>
        /// <returns>string</returns>
        public string CodePassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// <para>Resumo: Assynchronous method responsible for created a new user without duplication in the database</para>
        /// </summary>
        /// <param name="dto">NewUserDTO</param>
        public async Task CreatedUserWithoutDuplicateAsync(NewUserDTO dto)
        {
            var user = await _repository.GetUserByEmail(dto.Email);
            if (user != null) throw new Exception("This email is already being used");
            dto.Password = CodePassword(dto.Password);
            await _repository.AddUserAsync(dto);
        }

        /// <summary>
        /// <para>Resume: Assynchronous method to genereted a token</para>
        /// </summary>
        /// <param name="user">UserModel</param>
        public string GeneretedToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email.ToString()),
                new Claim(ClaimTypes.Role, user.Type.ToString())
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature
            )
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// <para>Resume: Assynchronous method responsible for give authorization to user authenticate</para>
        /// </summary>
        /// <param name="authentication">AuthenticationDTO</param>
        /// <returns>AuthenticationDTO</returns>
        /// <exception cref="Exception">User not found</exception>
        /// <exception cref="Exception">Bad password</exception>
        public async Task<AuthorizationDTO> GetAuthorizationAsync(AuthenticationDTO authentication)
        {
            var user = await _repository.GetUserByEmail(authentication.Email);
            if (user == null) throw new Exception("User Not Found");
            if (user.Password != CodePassword(authentication.Password)) throw new
            Exception("Bad Password");
            return new AuthorizationDTO(user.Id, user.Email, user.Type,
            GeneretedToken(user));
        }
        #endregion
    }
}