using LivrosQueJaLi.Services;
using NUnit.Framework;
using System.Threading.Tasks;

namespace TesteLivrosQueJaLi.Services
{
    [TestFixture]
    public class TesteGoogleBooksClient
    {
        private GoogleBooksClient _client;

        [OneTimeSetUp]
        public void SetUp()
        {
            _client = new GoogleBooksClient();
        }

        [TestCase]
        public async Task TesteGetBookForIdValido()
        {
            var str = await _client.GetBookForId("N0T0CgAAQBAJ");

            Assert.IsNotNull(str);
        }

        [TestCase]
        public async Task TesteGetBookForIdInvalido()
        {
            var str = await _client.GetBookForId("asdfasdfasdf");

            Assert.IsNull(str);
            //Assert.AreEqual("{}", str.Trim());
        }

        [TestCase]
        public async Task TesteGetBooksSearchSemParametro()
        {
            var str = await _client.GetBooks();

            Assert.IsNotNull(str);
        }

        [TestCase]
        public async Task TesteGetBooksSearchComParametroSemEspaco()
        {
            var str = await _client.GetBooks("Xamarin");

            Assert.IsNotNull(str);
        }

        [TestCase]
        public async Task TesteGetBooksSearchComParametroComEspaco()
        {
            var str = await _client.GetBooks("Xamarin Forms");

            Assert.IsNotNull(str);
        }
    }
}
