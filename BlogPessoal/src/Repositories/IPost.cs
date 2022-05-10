using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;

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
        void AddPost(AddPostDTO post);
        void UpdatePost(UpdatePostDTO post);
        void DeletePost(int Id);
        PostModel GetPostById(int Id);
        List<PostModel> GetAllPosts();
        List<PostModel> GetPostsBySearch(string title, string description, string emailCreator);

           
    }
}
