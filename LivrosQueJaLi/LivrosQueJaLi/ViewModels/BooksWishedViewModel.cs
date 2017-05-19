using LivrosQueJaLi.DAL;
using System.Threading.Tasks;

namespace LivrosQueJaLi.ViewModels
{
    public class BooksWishedViewModel : BaseUserBookViewModel
    {
        private UserBookDAL _userBookDAL = new UserBookDAL();

        protected override async Task FillObservableCollectionAsync()
        {
            var userBooks = await _userBookDAL.SelectUserBooksAsync(User.Id, true);

            Books.Clear();
            foreach (var book in userBooks)
                Books.Add(book.Book);
        }
    }
}
