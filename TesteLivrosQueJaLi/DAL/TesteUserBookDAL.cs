using LivrosQueJaLi.DAL;
using NUnit.Framework;
using System.Threading.Tasks;

namespace TesteLivrosQueJaLi.DAL
{
    [TestFixture]
    public class TesteUserBookDAL
    {
        private UserBookDAL _userBookDAL;

        [OneTimeSetUp]
        public void SetUp()
        {
            _userBookDAL = new UserBookDAL();
        }

        [TestCase]
        public async Task SelectUserBooksRead()
        {
            var usrBks = await _userBookDAL.SelectUserBooksAsync("afb376f1-3198-49d8-83a5-b8a8e86ae741");

            Assert.IsNotNull(usrBks);
        }

        [TestCase]
        public async Task SelectUserBooksWish()
        {
            var usrBks = await _userBookDAL.SelectUserBooksAsync("afb376f1-3198-49d8-83a5-b8a8e86ae741", true);

            Assert.IsNotNull(usrBks);
        }
    }
}
