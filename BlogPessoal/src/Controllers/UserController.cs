using BlogPessoal.src.Dtos;
using BlogPessoal.src.Repositorys;
using BlogPessoal.src.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogPessoal.src.controladores
{
    [ApiController]
    [Route("api/Usuarios")]
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
        #region Méthds

        [HttpGet("id/{idUser}")]
        [Authorize(Roles ="NORMAL,ADMINISTRATOR")]
        public IActionResult GetUserById([FromRoute] int id)
        {
            var user = _repository.GetUserById(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpGet]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public IActionResult GetUserByUsername([FromQuery] string username)
        {
            var users = _repository.GetUserByUsername(username);
            if (users.Count < 1) return NoContent();
            return Ok(users);
        }

        [HttpGet("email/{emailUsuario}")]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public IActionResult GetUserByEmail([FromRoute] string email)
        {
            var user = _repository.GetUserByEmail(email);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult AddUser([FromBody] NewUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                _services.CreatedUserWithoutDuplicate(user);
                return Created($"api/Usuarios/email/{user.Email}", user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Roles = "NORMAL,ADMINISTRATOR")]
        public IActionResult UserUpdate([FromBody] UserUpdateDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();
            user.Password = _services.CodePassword(user.Password);
            _repository.UserUpdate(user);
            return Ok(user);
        }

        [HttpDelete("deletar/{idUsuario}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult UserDelete([FromRoute] int idUser)
        {
            _repository.UserDelete(idUser);
            return NoContent();

        }
        #endregion
    }
}

