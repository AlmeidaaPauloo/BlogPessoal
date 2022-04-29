using System.ComponentModel.DataAnnotations;

namespace BlogPessoal.src.Dtos
{
    /// <summary>
    /// <para>Resume: Mirror class responsible for create a new post</para>
    /// <para>Created by: Paulo Almeida</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public class AddPostDTO
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public string Picture { get; set; }

        [Required]
        [StringLength(50)]
        public string EmailCreator { get; set; }

        [Required]
        [StringLength(50)]
        public string DescriptionTheme { get; set; }


        public AddPostDTO(string title, string description, string picture, string emailcreator, string descriptiontheme )
        {
            Title = title;
            Description = description;
            Picture = picture;
            EmailCreator = emailcreator;
            DescriptionTheme = descriptiontheme;
        }

    }
    /// <summary>
    /// <para>Resume: Mirror class responsible for change a post</para>
    /// <para>Created by: Paulo Almeida</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public class UpdatePostDTO
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        public string Picture { get; set; }

        [Required]
        [StringLength(50)]
        public string DescriptionTheme { get; set; }
        public UpdatePostDTO(string title, string description, string picture, string descriptiontheme)
        {
            Title = title;
            Description = description;
            Picture = picture;
            DescriptionTheme = descriptiontheme;
        }
    }
}
