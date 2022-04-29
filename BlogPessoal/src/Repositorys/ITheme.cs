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
        void addTheme(NewThemeDTO theme);
        void UserUpdate(ThemeUpdateDTO theme);
        void ThemeDelete(int id);
        ThemeModel GetUserById(int id);
        List<ThemeModel> PegarTemaPelaDescricao(string descricao);

    }
}
