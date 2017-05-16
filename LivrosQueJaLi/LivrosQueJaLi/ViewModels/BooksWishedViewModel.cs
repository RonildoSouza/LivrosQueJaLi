using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using MvvmHelpers;
using System.Threading.Tasks;

namespace LivrosQueJaLi.ViewModels
{
    public class BooksWishedViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Book> Books { get; private set; }
        private UserBookDAL _userBookDAL;

        public BooksWishedViewModel()
        {
            Books = new ObservableRangeCollection<Book>();
            _userBookDAL = new UserBookDAL();
        }

        public async Task FillBooksAsync() => await FillListView(FillAsync);

        private async Task FillAsync()
        {
            var userBooks = await _userBookDAL.SelectUserBooksAsync(User.Id, true);

            Books.Clear();
            foreach (var book in userBooks)
                Books.Add(book.Book);
        }
    }
}
