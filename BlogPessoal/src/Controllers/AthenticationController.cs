using System;
using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace BlogPessoal.src.controladores
{
    [ApiController]
    [Route("api/Authentication")]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        #region Attributes
        private readonly IAuthentication _services;
        #endregion
        #region Constructors
        public AuthenticationController(IAuthentication services)
        {
            _services = services;
        }
        #endregion
        #region Métodos

        /// <summary>
        /// User Authenticate 
        /// </summary>
        /// <param name="Authenticate">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="201">Return authenticate</response>
        /// <response code="400">Error on requisition</response>
        /// <response code="401">Not authorized</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody] AuthenticationDTO authentication)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var authorization = _services.GetAuthorizationAsync(authentication);
                return Ok(authorization);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        #endregion
    }
}