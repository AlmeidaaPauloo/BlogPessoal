using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.Dtos
{
    /// <summary>
    /// <para>Resume: Mirror class responsible for created a new theme</para>
    /// <para>Created by: Paulo Almeida</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public class NewThemeDTO
    {
        [Required]
        [StringLength(50)]
        public string Description { get; set; }
        
        public NewThemeDTO(string description)
        {
            Description = description;
        }
    }
    /// <summary>
    /// <para>Resume: Mirror class responsible for change a theme</para>
    /// <para>Created by: Paulo Almeida</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public class ThemeUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public ThemeUpdateDTO(string description, int id)
        {
            Id = id;
            Description = description;    
        }
    }
}
