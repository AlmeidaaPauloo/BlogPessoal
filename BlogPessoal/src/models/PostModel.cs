using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoal.src.models
{
    /// <summary>
    /// <para>Resumo: Class responsible for represent tb_posts in Database.</para>
    /// <para>Created by: Paulo Almeida</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 13/05/2022</para>
    /// </summary>
    [Table("tb_posts")]
    public class PostModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }   

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public string Picture { get; set; }

        [ForeignKey("fk_user")]
        public UserModel Creator { get; set; }

        [ForeignKey("fk_theme")]
        public ThemeModel Theme { get; set; }

    }
}
