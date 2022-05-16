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
        public string CodePassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }
        public async Task CreatedUserWithoutDuplicateAsync(NewUserDTO dto)
        {
            var user = await _repository.GetUserByEmail(dto.Email);
            if (user != null) throw new Exception("This email is already being used");
            dto.Password = CodePassword(dto.Password);
            await _repository.AddUserAsync(dto);
        }
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