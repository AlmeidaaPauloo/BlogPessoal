using BlogPessoal.src.utilities;
using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.Dtos
{
    /// <summary>
    /// <para>Resumo: Mirror class to create a new user</para>
    /// <para>Created By: Paulo Almeida</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public class NewUserDTO
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public string Picture { get; set; }

        [Required]
        public UserType Type { get; set; }

        public NewUserDTO(string name, string email, string password, string picture)
        {
            Name = name;
            Email = email;
            Password = password;
            Picture = picture;
            
        }
    }


    /// <summary>
    /// <para>Resume: Responsible for representing user CRUD actions</para>
    /// <para>Created by: Paulo Almeida</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public class UserUpdateDTO
    {
        [Required]
        [StringLength(50)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(200)]
        public string Password { get; set; }

        public string Picture { get; set; }

        public UserUpdateDTO(string name, string password, string picture, int id)
        {
            Id = id;
            Name = name;
            Password = password;
            Picture = picture;
        }
    }
}
