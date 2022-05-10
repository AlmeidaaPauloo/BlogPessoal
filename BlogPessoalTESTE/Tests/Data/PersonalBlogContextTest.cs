using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogPessoal.src.Data;
using Microsoft.EntityFrameworkCore;
using BlogPessoal.src.models;
using System.Linq;

namespace BlogPessoalTeste.Tests.Data
{
    [TestClass]
    public class PersonalBlogContextTest
    {
        private PersonalBlogContext _context;
         
        [TestInitialize]
        public void inicio()
        {
            var opt = new DbContextOptionsBuilder<PersonalBlogContext>()
                .UseInMemoryDatabase(databaseName: "db_PersonalBlog")
                .Options;

           _context = new PersonalBlogContext(opt);
        }
         
        [TestMethod]
        public void InsertNewUserInDBReturnUser()
        {
            UserModel User = new UserModel();

            User.Name = "Karol Boaz";
            User.Email = "Karol@email.com";
            User.Password = "123456";
            User.Picture = "THEREISTHEPICTURELINK";

            _context.Users.Add(User);
            _context.SaveChanges();

            Assert.IsNotNull(_context.Users.FirstOrDefault(u => u.Email == "Karol@email.com"));

        }

    }
}
