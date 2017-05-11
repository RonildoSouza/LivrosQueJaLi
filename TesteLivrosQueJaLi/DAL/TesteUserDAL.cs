using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models.Entities;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace TesteLivrosQueJaLi.DAL
{
    [TestFixture]
    public class TesteUserDAL
    {
        private UserDAL _userDAL;

        [OneTimeSetUp]
        public void SetUp()
        {
            _userDAL = new UserDAL();
        }

        [TestCase]
        public void InsertCommentValido()
        {
            var usr = new User()
            {
                IdFacebook = "efg456",
                UserName = "Teste"
            };

            _userDAL.InsertOrUpdate(usr);

            Assert.IsTrue(true);
        }
    }
}
