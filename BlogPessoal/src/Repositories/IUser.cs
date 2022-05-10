using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;

namespace BlogPessoal.src.Repositorys
{   
    ///<sumary>
    ///<para> Resumo: Responsavel por representar ações de CRUD de usuario<para>
    ///</para> Criado por: Paulo Almeida <para>
    ///<para> Versão: 1.0 <para>
    ///<para> Data: 29/04/2022 <para>
    ///<sumary>
    
    public interface IUser
    {
        void AddUser(NewUserDTO user);
        void UserUpdate(UserUpdateDTO user);
        void UserDelete(int id);
        UserModel GetUserById(int id);
        UserModel GetUserByEmail(string email);
        List<UserModel> GetUserByUsername(string username);

    }
}
