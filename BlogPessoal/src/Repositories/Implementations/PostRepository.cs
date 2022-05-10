using BlogPessoal.src.Data;
using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.Repositorys;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        #region

        public PostModel GetPostById(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id);
        }

        public List<PostModel> GetAllPosts()
        {
            return _context.Posts.ToList();
        }
        public List<PostModel> GetPostsBySearch(
            string title,
            string descriptionTheme,
            string nameCreator)
        {
            switch (title, descriptionTheme, nameCreator)
            {
                case (null, null, null):
                    return GetAllPosts();
                case (null, null, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Creator.Name.Contains(nameCreator))
                    .ToList();
                case (null, _, null):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Theme.Description.Contains(descriptionTheme))
                    .ToList();
                case (_, null, null):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p => p.Title.Contains(title))
                    .ToList();
                case (_, _, null):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Theme.Description.Contains(descriptionTheme))
                    .ToList();
                case (null, _, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Theme.Description.Contains(descriptionTheme) &
                    p.Creator.Name.Contains(nameCreator))
                    .ToList();
                case (_, null, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) &
                    p.Creator.Name.Contains(nameCreator))
                    .ToList();
                case (_, _, _):
                    return _context.Posts
                    .Include(p => p.Theme)
                    .Include(p => p.Creator)
                    .Where(p =>
                    p.Title.Contains(title) |
                    p.Theme.Description.Contains(descriptionTheme) |
                    p.Creator.Name.Contains(nameCreator))
                    .ToList();
            }
        }

        public void AddPost(AddPostDTO post)
        {
            _context.Posts.Add(new PostModel
            {
                Title = post.Title,
                Description = post.Description,
                Picture = post.Picture,
                Creator = _context.Users.FirstOrDefault(
                u => u.Email == post.EmailCreator),
                Theme = _context.Themes.FirstOrDefault(
                t => t.Description == post.DescriptionTheme)
            });
            _context.SaveChanges();
        }


        public void UpdatePost(UpdatePostDTO post)
        {
            var postExistent = GetPostById(post.Id);
            postExistent.Title = post.Title;
            postExistent.Description = post.Description;
            postExistent.Picture = post.Picture;
            postExistent.Theme = _context.Themes.FirstOrDefault(
            t => t.Description == post.DescriptionTheme);
            _context.Posts.Update(postExistent);
            _context.SaveChanges();
        }

         public void DeletePost(int id)
         {
                _context.Posts.Remove(GetPostById(id));
                _context.SaveChanges();
         }

        
        #endregion Métodos

    }
}
