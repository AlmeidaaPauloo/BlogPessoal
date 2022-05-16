using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task AddUserAsync(NewUserDTO user);
        Task UserUpdateAsync(UserUpdateDTO id);
        Task UserDeleteAsync(int id);
        Task<UserModel> GetUserByIdAsync(int id);
        Task<UserModel> GetUserByEmail(string email);
        Task<List<UserModel>> GetUserByUsernameAsync(string name);

    }
}
