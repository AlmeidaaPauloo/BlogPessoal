using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.Repositorys
{
    /// <summary>
    /// <para>Resume: Mirror class responsible for created a new post</para>
    /// <para>Created by: Paulo Almeida</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public interface IPost
    {
        Task AddPostAsync(AddPostDTO post);
        Task UpdatePostAsync(UpdatePostDTO post);
        Task DeletePostAsync(int Id);
        Task<PostModel> GetPostByIdAsync(int Id);
        Task<List<PostModel>> GetAllPostsAsync();
        Task<List<PostModel>> GetPostsBySearchAsync(string title, string description, string emailCreator);

           
    }
}
