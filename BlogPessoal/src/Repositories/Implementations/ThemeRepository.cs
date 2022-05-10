using BlogPessoal.src.Data;
using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.Repositorys;
using System.Collections.Generic;
using System.Linq;

namespace BlogPessoal.src.Repositories.Implementations
{
    public class ThemeRepository : ITheme
    {
        #region Atributos
        private readonly PersonalBlogContext _context;
        #endregion Atributos

        #region Construtores
        public ThemeRepository(PersonalBlogContext context)
        {
            _context = context;
        }
        #endregion

        #region
        public void AddTheme(NewThemeDTO theme)
        {
            _context.Themes.Add(new ThemeModel
            {
                Description = theme.Description
            });
            _context.SaveChanges();
        }

        public ThemeModel GetThemeById(int id)
        {
            return _context.Themes.FirstOrDefault(u => u.Id == id);
        }

        public List<ThemeModel> GetThemeByDescription(string description)
        {
            return _context.Themes
                           .Where(u => u.Description.Contains(description))
                           .ToList();
        }

        public void ThemeDelete(int id)
        {
            _context.Themes.Remove(GetThemeById(id));
            _context.SaveChanges();
        }

        public void ThemeUpdate(ThemeUpdateDTO theme)
        {
            var themeExisting = GetThemeById(theme.Id);
            themeExisting.Description = theme.Description;
            _context.Themes.Update(themeExisting);
            _context.SaveChanges();
        }

        public List<ThemeModel>  GetAllThemes()
        {
            return _context.Themes.ToList();
        }
        #endregion
    }
}
