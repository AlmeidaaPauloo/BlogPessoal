using BlogPessoal.src.Data;
using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.Repositorys;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.Repositories.Implementations
{
    public class PostRepository : IPost
    {      
        
            #region Atributos
            private readonly PersonalBlogContext _context;
            #endregion Atributos

            #region Construtores
            public PostRepository(PersonalBlogContext context)
            {
                _context = context;
            }
        #endregion Construtores

        #region Méthods

        /// <summary>
        /// <para>Resume> Méthod assynchronous for get a post by id</para>
        /// </summary>
        /// <param name="id">Post id</param>
        /// <return>PostModel</return>
        public async Task<PostModel> GetPostByIdAsync(int id)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Get All posts
        /// </summary>
        /// <returns>List</returns>
        public async Task<List<PostModel>> GetAllPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        /// <summary>
        /// <para>Resume> Assynchronous méthod for get a post by search</para>
        /// </summary>
        /// <param name="title"></param>
        /// <param name="descriptionTheme"></param>
        /// <param name="nameCreator"></param>
        /// <returns>List</returns>
        public async Task<List<PostModel>> GetPostsBySearchAsync(
            string title,
            string descriptionTheme,
            string nameCreator)
        {
            switch (title, descriptionTheme, nameCreator)
            {
                case (null, null, null):
                    return await GetAllPostsAsync();

                case (null, null, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Creator.Name.Contains(nameCreator))
                    .ToListAsync();

                case (null, _, null):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Theme.Description.Contains(descriptionTheme))
                    .ToListAsync();

                case (_, null, null):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title))
                    .ToListAsync();

                case (_, _, null):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Theme.Description.Contains(descriptionTheme))
                    .ToListAsync();

                case (null, _, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Theme.Description.Contains(descriptionTheme) &
                    p.Creator.Name.Contains(nameCreator))
                    .ToListAsync ();

                case (_, null, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Creator.Name.Contains(nameCreator))
                    .ToListAsync();

                case (_, _, _):
                    return await _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) |
                    p.Theme.Description.Contains(descriptionTheme) |
                    p.Creator.Name.Contains(nameCreator))
                    .ToListAsync();
            }
        }

        /// <summary>
        /// <para>Resume:Assynchronous method responsible to save a new post</para>
        /// </summary>
        /// <param name="post">AddPostDTO</param>
        public async Task AddPostAsync(AddPostDTO post)
        {
            await _context.Posts.AddAsync(new PostModel
            {
                Title = post.Title,
                Description = post.Description,
                Picture = post.Picture,
                Creator = _context.Users.FirstOrDefault(
                u => u.Email == post.EmailCreator),
                Theme = _context.Themes.FirstOrDefault(
                t => t.Description == post.DescriptionTheme)
            });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume:Assynchronous method responsible to update a post</para>
        /// </summary>
        /// <param name="post">UpdatePostDTO</param>
        public async Task UpdatePostAsync(UpdatePostDTO post)
        {
            var postExistent = await GetPostByIdAsync(post.Id);
            postExistent.Title = post.Title;
            postExistent.Description = post.Description;
            postExistent.Picture = post.Picture;
            postExistent.Theme = _context.Themes.FirstOrDefault(
            t => t.Description == post.DescriptionTheme);
            _context.Posts.Update(postExistent);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume:Assynchronous method responsible to delet post</para>
        /// </summary>
        /// <param name="id">AddPostDTO</param>
        public async Task DeletePostAsync(int id)
         {
                _context.Posts.Remove(await GetPostByIdAsync(id));
                await _context.SaveChangesAsync();
         }

        
        #endregion Métodos

    }
}
