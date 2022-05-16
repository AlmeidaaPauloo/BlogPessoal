using BlogPessoal.src.Dtos;
using BlogPessoal.src.Repositorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogPessoal.src.Controllers
{
    [ApiController]
    [Route("api/Post")]
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
        public async Task<ActionResult> GetPostByIdAsync([FromRoute] int id)
        {
            var post = await _repository.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetPostsBySearchAsync([FromQuery] string title, string description, string emailCreator)
        {
            var posts = await _repository.GetPostsBySearchAsync(title, description, emailCreator);
            if (posts.Count < 1) return NoContent();
            return Ok(posts);
        }     

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddPostAsync([FromBody] AddPostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _repository.AddPostAsync(post);
            return Created($"api/Users/{post.Title}", post);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> PostUpdateAsync([FromBody] UpdatePostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _repository.UpdatePostAsync(post);
            return Ok(post);
        }

        [HttpDelete("delete/{idPost}")]
        [Authorize]
        public async Task<ActionResult> DeletePostAsync([FromRoute] int idPost)
        {
            await _repository.DeletePostAsync(idPost);
            return NoContent();

        }
        #endregion
    }
}
