using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.Repositorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Get Post by id
        /// </summary>
        /// <param name="GetPostByIdAsync">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Return a post</response>
        /// <response code="404">Post does not exist</response>
        [HttpGet("id/{idPost}")]
        [Authorize]
        public async Task<ActionResult> GetPostByIdAsync([FromRoute] int id)
        {
            var post = await _repository.GetPostByIdAsync(id);
            if (post == null) return NotFound();
            return Ok(post);
        }

        /// <summary>
        /// Get posts by search
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Post list</response>
        /// <response code="204">Empty list</response>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetPostsBySearchAsync([FromQuery] string title, string description, string emailCreator)
        {
            var posts = await _repository.GetPostsBySearchAsync(title, description, emailCreator);
            if (posts.Count < 1) return NoContent();
            return Ok(posts);
        }

        /// <summary>
        /// Add post
        /// </summary>
        /// <param name="post">NovaPostagemDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Example of requisition:
        ///
        ///     POST /api/Posts
        ///     {
        ///        "title": "Carro",
        ///        "descricaoTema": "Civic 2008",      
        ///        "nameCreator": "Paulo Almeida",      
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return post created</response>
        /// <response code="400">Error on requisition</response>
        /// <response code="401">DescriptionTheme already used</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddPostAsync([FromBody] AddPostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _repository.AddPostAsync(post);
            return Created($"api/Users/{post.Title}", post);
        }

        /// <summary>
        /// Update Post
        /// </summary>
        /// <param name="post">AtualizarPostagemDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Exemplo de requisição:
        ///
        ///     PUT /api/Post
        ///     {
        ///        "title": "Carro",
        ///        "description": "Civic 2008",
        ///        "picture": "URLPicture",
        ///        "theme": "Automoveis"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Return updated post</response>
        /// <response code="400">Error on requisition</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize]
        public async Task<ActionResult> PostUpdateAsync([FromBody] UpdatePostDTO post)
        {
            if (!ModelState.IsValid) return BadRequest();
            await _repository.UpdatePostAsync(post);
            return Ok(post);
        }

        /// <summary>
        /// Delete post by id
        /// </summary>
        /// <param name="idPost">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Deleted post</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idUser}")]        
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
