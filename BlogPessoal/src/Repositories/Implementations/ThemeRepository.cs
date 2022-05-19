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
    /// <summary>
    /// <para>Resume> Class responsibloy by implement ITheme</para>
    /// <para>Created by: Paulo Almeida</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 13/05/2022</para>
    /// </summary>
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

        #region Methods

        /// <summary>
        /// <para>Resume: Assynchronous method to update a theme</para>
        /// </summary>
        /// <param name="theme">AtualizarTemaDTO</param>
        public async Task AddThemeAsync(NewThemeDTO theme)
        {
            await _context.Themes.AddAsync(new ThemeModel
            {
                Description = theme.Description
            });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo> Assynchronous method to get a theme by id</para>
        /// </summary>
        /// <param nome="descricao">Id do tema</param>
        /// <return>TemaModelo</return>
        public async Task<ThemeModel> GetThemeByIdAsync(int id)
        {
            return await _context.Themes.FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// <para>Resume> Assynchronous method to get a theme by description</para>
        /// </summary>
        /// <param name="description">THeme id</param>
        /// <return>ThemeModel</return>
        public async Task<List<ThemeModel>> GetThemeByDescriptionAsync(string description)
        {
            return await _context.Themes
                           .Where(u => u.Description.Contains(description))
                           .ToListAsync();
        }

        /// <summary>
        /// <para>Resume: Assynchronous method to delet a theme</para>
        /// </summary>
        /// <param name="theme">Id theme</param>
        public async Task ThemeDeleteAsync(int id)
        {
            _context.Themes.Remove(await GetThemeByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume: Assynchronous method to update a theme</para>
        /// </summary>
        /// <param name="theme">AtualizarTemaDTO</param>
        public async Task ThemeUpdateAsync(ThemeUpdateDTO theme)
        {
            var themeExisting = await GetThemeByIdAsync(theme.Id);
            themeExisting.Description = theme.Description;
            _context.Themes.Update(themeExisting);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get all themes
        /// </summary>
        /// <returns>List</returns>
        public async Task<List<ThemeModel>>  GetAllThemesAsync()
        {
            return await _context.Themes.ToListAsync();
        }
        #endregion Methods
    }
}
