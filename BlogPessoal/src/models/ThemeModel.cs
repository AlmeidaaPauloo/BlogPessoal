using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BlogPessoal.src.models
{
    /// <summary>
    /// <para>Resumo: Class responsible for represent tb_thems io Database.</para>
    /// <para>Criado por: Paulo ALmeida</para>
    /// <para>Version: 1.0</para>
    /// <para>Data: 13/05/2022</para>
    /// </summary>
    [Table("tb_themes")]
    public class ThemeModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [JsonIgnore]
        public List<PostModel> RelationPosts { get; set; }
    }
}
