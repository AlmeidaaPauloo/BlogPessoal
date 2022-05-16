using BlogPessoal.src.Data;
using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using BlogPessoal.src.Repositorys;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task AddThemeAsync(NewThemeDTO theme)
        {
            await _context.Themes.AddAsync(new ThemeModel
            {
                Description = theme.Description
            });
            await _context.SaveChangesAsync();
        }

        public async Task<ThemeModel> GetThemeByIdAsync(int id)
        {
            return await _context.Themes.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<ThemeModel>> GetThemeByDescriptionAsync(string description)
        {
            return await _context.Themes
                           .Where(u => u.Description.Contains(description))
                           .ToListAsync();
        }

        public async Task ThemeDeleteAsync(int id)
        {
            _context.Themes.Remove(await GetThemeByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task ThemeUpdateAsync(ThemeUpdateDTO theme)
        {
            var themeExisting = await GetThemeByIdAsync(theme.Id);
            themeExisting.Description = theme.Description;
            _context.Themes.Update(themeExisting);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ThemeModel>>  GetAllThemesAsync()
        {
            return await _context.Themes.ToListAsync();
        }
        #endregion
    }
}
