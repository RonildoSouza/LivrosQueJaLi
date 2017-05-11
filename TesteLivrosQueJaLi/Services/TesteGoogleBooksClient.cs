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
            var book = await _client.GetBookForId("N0T0CgAAQBAJ");

            Assert.IsNotNull(book);
        }

        [TestCase]
        public async Task TesteGetBookForIdInvalido()
        {
            var book = await _client.GetBookForId("asdfasdfasdf");

            Assert.IsNull(book);
        }

        [TestCase]
        public async Task TesteGetBooksSearchSemParametro()
        {
            var book = await _client.GetBooks();

            Assert.IsNotNull(book);
        }

        [TestCase]
        public async Task TesteGetBooksSearchComParametroSemEspaco()
        {
            var book = await _client.GetBooks("a");

            Assert.IsNotNull(book);
        }

        [TestCase]
        public async Task TesteGetBooksSearchComParametroComEspaco()
        {
            var book = await _client.GetBooks("Xamarin Forms");

            Assert.IsNotNull(book);
        }
    }
}
