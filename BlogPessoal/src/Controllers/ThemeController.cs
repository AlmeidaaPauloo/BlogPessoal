using BlogPessoal.src.Dtos;
using BlogPessoal.src.Repositorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BlogPessoal.src.controllers
{
    [ApiController]
    [Route("api/Users")]
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

        [HttpGet("id/{idTheme}")]
        [Authorize]
        public IActionResult GetThemeById([FromRoute] int id)
        {
            var theme = _repository.GetThemeById(id);
            if (theme == null) return NotFound();
            return Ok(theme);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetThemeByDescription([FromQuery] string description)
        {
            var Theme = _repository.GetThemeByDescription(description);
            if (Theme.Count < 1) return NoContent();
            return Ok(Theme);
        }
     
        [HttpPost]
        [Authorize]
        public IActionResult AddTheme([FromBody] NewThemeDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.AddTheme(theme);
            return Created($"api/Theme/{theme.Description}", theme);
        }

        [HttpPut]
        [Authorize(Roles ="ADMINISTRADOR")]
        public IActionResult ThemeUpdate([FromBody] ThemeUpdateDTO theme)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.ThemeUpdate(theme);
            return Ok(theme);
        }

        [HttpDelete("deletar/{idUsuario}")]
        [Authorize(Roles ="ADMINISTRADOR")]
        public IActionResult ThemeDelete([FromRoute] int idTheme)
        {
            _repository.ThemeDelete(idTheme);
            return NoContent();

        }
        #endregion
    }
}