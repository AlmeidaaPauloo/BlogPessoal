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
        public string Descricao { get; set; }

        public string Foto { get; set; }

        [ForeignKey("fk_user")]
        public UserModel Criator { get; set; }

        [ForeignKey("fk_theme")]
        public ThemeModel Theme { get; set; }

    }
}
