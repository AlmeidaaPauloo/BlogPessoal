using BlogPessoal.src.Dtos;
using BlogPessoal.src.Repositorys;
using BlogPessoal.src.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BlogPessoal.src.controladores
{

    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        #region Attributes
        private readonly IUser _repository;
        private readonly IAuthentication _services;
        #endregion
        #region Constructors
        public UserController(IUser repository, IAuthentication services)
        {
            _repository = repository;
            _services = services;
        }
        #endregion
        #region Méthods

        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return a user</response>
        /// <response code="404">User does not exist</response>
        [HttpGet("id/{idUser}")]
        [Authorize(Roles ="NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByIdAsync([FromRoute] int id)
        {
            var user = await _repository.GetUserByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Get User by name
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return the user</response>
        /// <response code="204">Name does not exist</response>
        [HttpGet]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByUsernameAsync([FromQuery] string name)
        {
            var users = await _repository.GetUserByUsernameAsync(name);
            if (users.Count < 1) return NoContent();
            return Ok(users);
        }

        /// <summary>
        /// Get User by Email
        /// </summary>
        /// <param name="email">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return a email</response>
        /// <response code="404">Email does not exist</response>
        [HttpGet("email/{emailUser}")]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByEmailAsync([FromRoute] string email)
        {
            var user = await _repository.GetUserByEmail(email);
            if (user == null) return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Add a new User
        /// </summary>
        /// <param name="user">NewUserDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// requisition example:
        ///
        ///     POST /api/Users
        ///     {
        ///        "name": "Paulo Boaz",
        ///        "email": "paulo@email.com",
        ///        "password": "134652",
        ///        "picture": "URLPICTURE",
        ///        "type": "NORMAL"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return a user created</response>
        /// <response code="400">Error in requisition</response>
        /// <response code="401">E-mail already registered</response>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddUserAsync([FromBody] NewUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
               await _services.CreatedUserWithoutDuplicateAsync(user);
                return Created($"api/Users/email/{user.Email}", user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        /// <summary>
        /// User Update
        /// </summary>
        /// <param name="user">UserUpdateDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// requisition example:
        ///
        ///     PUT /api/Users
        ///     {
        ///        "id": 1,    
        ///        "name": "Paulo Boaz",
        ///        "password": "134652",
        ///        "picture": "URLPICTURE"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return a user updated</response>
        /// <response code="400">Error in requisition</response>      
        [HttpPut]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public async Task<ActionResult> UserUpdateAsync([FromBody] UserUpdateDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();
            user.Password = _services.CodePassword(user.Password);
            await _repository.UserUpdateAsync(user);
            return Ok(user);
        }

        /// <summary>
        /// User delete by id
        /// </summary>
        /// <param name="idUser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">User  deleted</response>
        [HttpDelete("deletar/{idUsuario}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> UserDeleteAsync([FromRoute] int idUser)
        {
            await _repository.UserDeleteAsync(idUser);
            return NoContent();

        }
        #endregion
    }
}

