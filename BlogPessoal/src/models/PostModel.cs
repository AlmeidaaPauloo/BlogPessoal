using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogPessoal.src.models
{
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
