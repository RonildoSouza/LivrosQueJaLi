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
            var book = await _client.GetBookByIdAsync("N0T0CgAAQBAJ");

            Assert.IsNotNull(book);
        }

        [TestCase]
        public async Task TesteGetBookForIdInvalido()
        {
            var book = await _client.GetBookByIdAsync("asdfasdfasdf");

            Assert.IsNull(book);
        }

        [TestCase]
        public async Task TesteGetBooksSearchSemParametro()
        {
            var book = await _client.GetBooksAsync();

            Assert.IsNotNull(book);
        }

        [TestCase]
        public async Task TesteGetBooksSearchComParametroSemEspaco()
        {
            var book = await _client.GetBooksAsync("a");

            Assert.IsNotNull(book);
        }

        [TestCase]
        public async Task TesteGetBooksSearchComParametroComEspaco()
        {
            var book = await _client.GetBooksAsync("Xamarin Forms");

            Assert.IsNotNull(book);
        }
    }
}
