using BlogPessoal.src.Data;
using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPessoal.src.Repositorys.Implementations
{
    /// <summary>
    /// <para>Resume: Mirror class responsible for implemented IUser</para>
    /// <para>Created by: Paulo Almeida</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 02/05/2022</para>
    /// </summary>
    public class UserRepository : IUser
    {
        #region Atributos
        
        private readonly PersonalBlogContext _context;

        #endregion

        #region Constructors

        public UserRepository(PersonalBlogContext context)
        {
            _context = context;
        }
        #endregion Construtores


        #region Metodos

        /// <summary>
        /// <para>Resumo: Async Méthod for save a new user</para>
        /// </summary>
        /// <param name="user">NewUserDTO</param>
        public async Task AddUserAsync(NewUserDTO user)
        {
            _context.Users.Add(new UserModel
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Picture = user.Picture,
                Type = user.Type,
            });
            await  _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Async Méthod for get a user by email</para>
        /// </summary>
        /// <param name="email">Email of user</param>
        /// <return>UserModel</return>
        public async Task<UserModel> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// <para>Resumo: Async Méthod for get a user by Id</para>
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <return>UserModel</return>
        public async Task<UserModel> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// <para>Resumo: Async Méthod for get a user by name</para>
        /// </summary>
        /// <param name="email">Email do usuario</param>
        /// <return>UsuarioModelo</return>
        public async Task<List<UserModel>> GetUserByUsernameAsync(string name)
        {
            return await _context.Users
                        .Where(u => u.Name.Contains(name))
                        .ToListAsync();
        }

        /// <summary>
        /// <para>Resumo: Async Method for delete a user</para>
        /// </summary>
        /// <param name="id">Id of user</param>
        public async Task UserDeleteAsync(int id)
        {
            _context.Users.Remove(await GetUserByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resumo: Async Method for update a user</para>
        /// </summary>
        /// <param name="user">UserUpdateDTO</param>
        public async Task UserUpdateAsync(UserUpdateDTO user)
        {
            var userModel = await GetUserByIdAsync(user.Id);
            userModel.Name = user.Name;
            userModel.Password = user.Password;
            userModel.Picture = user.Picture;            
            _context.Users.Update(userModel);
            await _context.SaveChangesAsync();
        }

       

        #endregion Methods
    }
}
