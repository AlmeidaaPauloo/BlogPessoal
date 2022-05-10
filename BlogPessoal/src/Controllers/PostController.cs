using BlogPessoal.src.Dtos;
using BlogPessoal.src.Repositorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.Controllers
{
    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]
    public class PostController : ControllerBase    
    {
        #region Attributes
        private readonly IPost _repository;
        #endregion
        #region Constructors
        public PostController(IPost repository)
        {
            _repository = repository;
        }
        #endregion
        #region Méthds

        [HttpGet("id/{idPost}")]
        [Authorize]
        public IActionResult GetPostById([FromRoute] int id)
        {
            var post = _repository.GetPostById(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetPostsBySearch([FromQuery] string title, string description, string emailCreator)
        {
            var posts = _repository.GetPostsBySearch(title, description, emailCreator);
            if (posts.Count < 1) return NoContent();
            return Ok(posts);
        }     

        [HttpPost]
        [Authorize]
        public IActionResult AddPost([FromBody] AddPostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.AddPost(post);
            return Created($"api/Users/{post.Title}", post);
        }

        [HttpPut]
        [Authorize]
        public IActionResult PostUpdate([FromBody] UpdatePostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.UpdatePost(post);
            return Ok(post);
        }

        [HttpDelete("delete/{idPost}")]
        [Authorize]
        public IActionResult DeletePost([FromRoute] int idPost)
        {
            _repository.DeletePost(idPost);
            return NoContent();

        }
        #endregion
    }
}
