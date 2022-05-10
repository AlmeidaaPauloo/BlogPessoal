using BlogPessoal.src.Data;
using BlogPessoal.src.Dtos;
using BlogPessoal.src.Repositorys;
using BlogPessoal.src.Repositorys.Implementations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;


namespace BlogPessoalTeste.Tests.Repository
{
    [TestClass]
    internal class UserRepositoryTest
    {
        private PersonalBlogContext _context;
        private IUser _repository;
        [TestInitialize]
        public void InitialConfiguration()
        {
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
            .UseInMemoryDatabase(databaseName: "db_blogpessoal")
            .Options;
            _context = new PersonalBlogContext(opt);
            _repository = new UserRepository(_context);
        }
        [TestMethod]
        public void CreateFourUsersInDataBaseReturnFourUsers()
        {
            //GIVEN - Dado que registro 4 usuarios no banco
            _repository.AddUser(
            new NewUserDTO(
            "Gustavo Boaz",
            "gustavo@email.com",
            "134652",
            "URLFOTO"));
            _repository.AddUser(
            new NewUserDTO(
            "Mallu Boaz",
            "mallu@email.com",
            "134652",
            "URLFOTO"));
            _repository.AddUser(
            new NewUserDTO(
            "Catarina Boaz",
            "catarina@email.com",
            "134652",
            "URLFOTO"));
            _repository.AddUser(
            new NewUserDTO(
            "Pamela Boaz",
            "pamela@email.com",
            "134652",
            "URLFOTO"));
            //WHEN - Quando pesquiso lista total
            //THEN - Então recebo 4 usuarios
            Assert.AreEqual(4, _context.Users.Count());
        }
        [TestMethod]
        public void GetUserByEmailReturnNotNull()
        {
            //GIVEN - Dado que registro um usuario no banco
            _repository.AddUser(
            new NewUserDTO(
            "Zenildo Boaz",
            "zenildo@email.com",
            "134652",
            "URLFOTO"));
            //WHEN - Quando pesquiso pelo email deste usuario
            var user = _repository.GetUserByEmail("zenildo@email.com");
            //THEN - Então obtenho um usuario
            Assert.IsNotNull(user);
        }
        [TestMethod]
        public void GetUserByIdReturnNotNullUserName()
        {
            //GIVEN - Dado que registro um usuario no banco
            _repository.AddUser(
            new NewUserDTO(
            "Neusa Boaz",
            "neusa@email.com",
            "134652",
            "URLFOTO"));
            //WHEN - Quando pesquiso pelo id 6
            var user = _repository.GetUserById(6);
            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(user);
            //THEN - Então, o elemento deve ser Neusa Boaz
            Assert.AreEqual("Neusa Boaz", user.Name);
        }
        [TestMethod]
        public void UpdateUserReturnUserUpdated()
        {
            //GIVEN - Dado que registro um usuario no banco
            _repository.AddUser(
            new NewUserDTO(
            "Estefânia Boaz",
            "estefania@email.com",
            "134652",
            "URLFOTO"));
            //WHEN - Quando atualizamos o usuario
            var antigo =
            _repository.GetUserByEmail("estefania@email.com");
            _repository.UserUpdate(
            new UserUpdateDTO(
            "Estefânia Moura",
            "123456",
            "URLFOTO",
            7));
            //THEN - Então, quando validamos pesquisa deve retornar nome
            
            Assert.AreEqual(
            "Estefânia Moura",
            _context.Users.FirstOrDefault(u => u.Id == antigo.Id).Name);
            //THEN - Então, quando validamos pesquisa deve retornar senha 123456
            Assert.AreEqual(
            "123456",
            _context.Users.FirstOrDefault(u => u.Id ==
            antigo.Id).Password);
        }
    }
}
