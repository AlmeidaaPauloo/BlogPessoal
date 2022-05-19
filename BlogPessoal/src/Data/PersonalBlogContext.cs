using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;

namespace BlogPessoal.src.Data
{
    public class PersonalBlogContext : DbContext
    {
        /// <summary>
        /// <para>Resume> Context class, responsible by loading context and setting Dbsets</para>
        /// <para>Created by: Paulo Almeida</para>
        /// <para>Version: 1.0</para>
        /// <para>Date: 13/05/2022</para>
        /// </summary>
        public DbSet<UserModel> Users { get; set; }
           public DbSet<ThemeModel> Themes { get; set; }
           public DbSet<PostModel> Posts { get; set; }  

           public PersonalBlogContext(DbContextOptions<PersonalBlogContext> opt) : base(opt)
           {

           }
    }
}
