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

namespace BlogPessoal.src.services.Implementations
{
    public class AuthenticationServices : IAuthentication
    {
        #region Attributes
        private readonly IUser _repositorio;
        public IConfiguration Configuration { get; }
        #endregion
        #region Constructors
        public AuthenticationServices(IUser repositorio, IConfiguration
        configuration)
        {
            _repositorio = repositorio;
            Configuration = configuration;
        }
        #endregion

        #region Methods
        public string CodePassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }
        public void CreatedUserWithoutDuplicate(NewUserDTO dto)
        {
            var user = _repositorio.GetUserByEmail(dto.Email);
            if (user != null) throw new Exception("This email is already being used");
            dto.Password = CodePassword(dto.Password);
            _repositorio.AddUser(dto);
        }
        public string GeneretedToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            var tokenDescricao = new SecurityTokenDescriptor
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
            var token = tokenHandler.CreateToken(tokenDescricao);
            return tokenHandler.WriteToken(token);
        }
        public AuthorizationDTO GetAuthorization(AuthenticationDTO authentication)
        {
            var user = _repositorio.GetUserByEmail(authentication.Email);
            if (user == null) throw new Exception("User Not Found");
            if (user.Password != CodePassword(authentication.Password)) throw new
            Exception("Bad Password");
            return new AuthorizationDTO(user.Id, user.Email, user.Type,
            GeneretedToken(user));
        }

        #endregion
    }
}