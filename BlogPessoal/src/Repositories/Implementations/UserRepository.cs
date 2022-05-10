using BlogPessoal.src.Data;
using BlogPessoal.src.Dtos;
using BlogPessoal.src.models;
using System.Collections.Generic;
using System.Linq;

namespace BlogPessoal.src.Repositorys.Implementations
{
    /// <summary>
    /// <para>Resume: Mirror class responsible for transporting a user to register</para>
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

        public void AddUser(NewUserDTO user)
        {
            _context.Users.Add(new UserModel
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Picture = user.Picture,
                Type = user.Type,
            });
        }

        public UserModel GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public UserModel GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public List<UserModel> GetUserByUsername(string name)
        {
            return _context.Users
                        .Where(u => u.Name.Contains(name))
                        .ToList();
        }

            public void UserDelete(int id)
        {
            _context.Users.Remove(GetUserById(id));
            _context.SaveChanges();
        }

        public void UserUpdate(UserUpdateDTO user)
        {
            var userModel = GetUserById(user.Id);
            userModel.Name = user.Name;
            userModel.Password = user.Password;
            userModel.Picture = user.Picture;            
            _context.Users.Update(userModel);
            _context.SaveChanges();
        }

       

        #endregion Methods
    }
}
