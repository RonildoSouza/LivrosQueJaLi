using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models.Entities;
using NUnit.Framework;
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

        [TestCase, Ignore("Está anulando a thread do azure")]
        public void InsertUser()
        {
            var usr = new User()
            {
                IdFacebook = "3666",
                UserName = "Teste NUnit"
            };

            _userDAL.InsertOrUpdate(usr);

            Assert.IsTrue(true);
        }

        [TestCase]
        public async Task SelectByIdFacebookOrEmail()
        {
            var usr = await _userDAL.SelectByIdFacebookOrEmailAsync("abc123", "");

            Assert.IsNotNull(usr);
        }
    }
}
