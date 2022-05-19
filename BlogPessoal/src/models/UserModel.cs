using BlogPessoal.src.utilities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.models
{
    /// <summary>
    /// <para>Resume> Class responsible for represent tb_users in the database</para>
    /// <para>Created by: Paulo Almeida</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 13/05/2022</para>
    /// </summary>
    [Table("tb_users")]
    public class UserModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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
        
        [JsonIgnore, InverseProperty("Creator")]
        public List<PostModel> MyPosts { get; set; }

    }
}
