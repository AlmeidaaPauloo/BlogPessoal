using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;

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
        void AddTheme(NewThemeDTO theme);
        void ThemeUpdate(ThemeUpdateDTO theme);
        void ThemeDelete(int id);
        ThemeModel GetThemeById(int id);
        List<ThemeModel> GetThemeByDescription(string description);
        List<ThemeModel> GetAllThemes();
    }
}
