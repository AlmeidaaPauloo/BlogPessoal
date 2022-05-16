using BlogPessoal.src.Dtos;
using BlogPessoal.src.Repositorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Themes")]
    [Produces("application/json")]
    public class ThemeController : ControllerBase
    {
        #region Attributes
        private readonly ITheme _repository;
        #endregion
        #region Constructors
        public ThemeController(ITheme repository)
        {
            _repository = repository;
        }
        #endregion
        #region Méthods

        /// <summary>
        /// Get Theme by Id
        /// </summary>
        /// <param name="id">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return a theme</response>
        /// <response code="404">Theme does not exist</response>
        [HttpGet("id/{idTheme}")]
        [Authorize]
        public IActionResult GetThemeById([FromRoute] int id)
        {
            var theme = _repository.GetThemeByIdAsync(id);
            if (theme == null) return NotFound();
            return Ok(theme);
        }

        /// <summary>
        /// Get theme by description
        /// </summary>
        /// <param name="description">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Retur the theme</response>
        /// <response code="204">Theme does not exist</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetThemeByDescriptionAsync([FromQuery] string description)
        {
            var Theme = await _repository.GetThemeByDescriptionAsync(description);
            if (Theme.Count < 1) return NoContent();
            return Ok(Theme);
        }

        /// <summary>
        /// Add a new Theme
        /// </summary>
        /// <param name="theme">NewUserDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// requisition example:
        ///
        ///     POST /api/Themes
        ///     {        ///        
        ///        "descriptionTheme": "Carros Usados"                
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return a theme created</response>
        /// <response code="400">Error in requisition</response>        
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddThemeAsync([FromBody] NewThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _repository.AddThemeAsync(theme);
            return Created($"api/Theme/{theme.Description}", theme);
        }

        [HttpPut]
        [Authorize(Roles ="ADMINISTRADOR")]
        public async Task<ActionResult> ThemeUpdateAsync([FromBody] ThemeUpdateDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _repository.ThemeUpdateAsync(theme);
            return Ok(theme);
        }

        [HttpDelete("deletar/{idUsuario}")]
        [Authorize(Roles ="ADMINISTRADOR")]
        public async Task<ActionResult> ThemeDeleteAsync([FromRoute] int idTheme)
        {
            await _repository.ThemeDeleteAsync(idTheme);
            return NoContent();

        }
        #endregion
    }
}