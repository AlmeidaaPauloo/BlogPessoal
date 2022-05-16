using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogPessoal.src.Repositorys
{
    /// <summary>
    /// <para>Resume: Responsible for representing user CRUD actions for theme</para>
    /// <para>Created by: Paulo Almeida</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 29/04/2022</para>
    /// </summary>
    public interface ITheme
    {
        Task AddThemeAsync(NewThemeDTO theme);
        Task ThemeUpdateAsync(ThemeUpdateDTO theme);
        Task ThemeDeleteAsync(int id);
        Task<ThemeModel> GetThemeByIdAsync(int id);
        Task<List<ThemeModel>> GetThemeByDescriptionAsync(string description);
        Task<List<ThemeModel>> GetAllThemesAsync();
    }
}
