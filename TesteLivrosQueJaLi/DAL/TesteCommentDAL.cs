using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models.Entities;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace TesteLivrosQueJaLi.DAL
{
    [TestFixture]
    public class TesteCommentDAL
    {
        private CommentDAL _commentDAL;

        [OneTimeSetUp]
        public void SetUp()
        {
            _commentDAL = new CommentDAL();
        }

        [TestCase]
        public async Task SelectBookCommentsComIdValido()
        {
            var books = await _commentDAL.SelectBookCommentsAsync("GmroDQAAQBAJ");

            Assert.IsTrue(books.Count != 0);
        }

        [TestCase]
        public async Task SelectBookCommentsComIdInValido()
        {
            var books = await _commentDAL.SelectBookCommentsAsync("asdasdasdasdasdwdads");

            Assert.IsTrue(books.Count == 0);
        }

        [TestCase, Ignore("Está anulando a thread do azure")]
        public void InsertCommentValido()
        {
            var com = new Comment()
            {
                IdBook = "N0T0CgAAQBAJ",
                IdUserComment = "Usuário NUnit",
                CommentText = "Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit, " +
                "Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit" +
                ", Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit...",
                CreatedAt = DateTime.Now
            };

            _commentDAL.InsertOrUpdate(com);

            Assert.IsTrue(true);
        }

        [TestCase, Ignore("Está anulando a thread do azure")]
        public void InsertCommentInValido()
        {
            _commentDAL.InsertOrUpdate(new Comment()
            {                   
                IdUserComment = "Usuário NUnit",
                CommentText = "Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit, " +
                "Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit" +
                ", Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit, Teste NUnit..."
            });

            Assert.IsTrue(true);
        }
    }
}
