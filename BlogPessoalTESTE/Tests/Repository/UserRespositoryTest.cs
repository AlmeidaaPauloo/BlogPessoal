using BlogPessoal.src.Data;
using BlogPessoal.src.Dtos;
using BlogPessoal.src.Repositorys;
using BlogPessoal.src.Repositorys.Implementations;
using BlogPessoal.src.utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task CreateFourUsersInDataBaseReturnFourUsers()
        {
            //GIVEN - Dado que registro 4 usuarios no banco
            await _repository.AddUserAsync(
            new NewUserDTO(
            "Gustavo Boaz",
            "gustavo@email.com",
            "134652",
            "URLFOTO",
            UserType.ADMINISTRATOR));
            await _repository.AddUserAsync(
            new NewUserDTO(
            "Mallu Boaz",
            "mallu@email.com",
            "134652",
            "URLFOTO",
            UserType.ADMINISTRATOR));
            await _repository.AddUserAsync(
            new NewUserDTO(
            "Catarina Boaz",
            "catarina@email.com",
            "134652",
            "URLFOTO",
            UserType.ADMINISTRATOR));
            await _repository.AddUserAsync(
            new NewUserDTO(
            "Pamela Boaz",
            "pamela@email.com",
            "134652",
            "URLFOTO",
            UserType.ADMINISTRATOR));
            //WHEN - Quando pesquiso lista total
            //THEN - Então recebo 4 usuarios
            Assert.AreEqual(4, _context.Users.Count());
        }
        [TestMethod]
        public async Task GetUserByEmailReturnNotNull()
        {
            //GIVEN - Dado que registro um usuario no banco
            await _repository.AddUserAsync (
            new NewUserDTO(
            "Zenildo Boaz",
            "zenildo@email.com",
            "134652",
            "URLFOTO",
            UserType.ADMINISTRATOR));
            //WHEN - Quando pesquiso pelo email deste usuario
            var user = _repository.GetUserByEmail("zenildo@email.com");
            //THEN - Então obtenho um usuario
            Assert.IsNotNull(user);
        }
        [TestMethod]
        public async Task GetUserByIdReturnNotNullUserNameAsync()
        {
            //GIVEN - Dado que registro um usuario no banco
            await _repository.AddUserAsync(
            new NewUserDTO(
            "Neusa Boaz",
            "neusa@email.com",
            "134652",
            "URLFOTO",
            UserType.ADMINISTRATOR));
            //WHEN - Quando pesquiso pelo id 6
            var user = await _repository.GetUserByIdAsync(6);
            //THEN - Então, deve me retornar um elemento não nulo
            Assert.IsNotNull(user);
            //THEN - Então, o elemento deve ser Neusa Boaz
            Assert.AreEqual("Neusa Boaz", user.Name);
        }
        [TestMethod]
        public async Task UpdateUserReturnUserUpdatedAsync()
        {
            //GIVEN - Dado que registro um usuario no banco
            await _repository.AddUserAsync(
            new NewUserDTO(
            "Estefânia Boaz",
            "estefania@email.com",
            "134652",
            "URLFOTO",
            UserType.ADMINISTRATOR));
            //WHEN - Quando atualizamos o usuario
            var antigo = 
            await _repository.GetUserByEmail("estefania@email.com");
            await _repository.UserUpdateAsync(
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
