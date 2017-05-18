using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using MvvmHelpers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class BooksWishedViewModel : BaseViewModel
    {
        private UserBookDAL _userBookDAL;

        public ObservableRangeCollection<Book> Books { get; private set; }

        public Command RefreshCommand { get; }

        public BooksWishedViewModel()
        {
            _userBookDAL = new UserBookDAL();
            Books = new ObservableRangeCollection<Book>();
            RefreshCommand = new Command(ExecuteRefreshCommand);
        }

        private void ExecuteRefreshCommand() => FillListView(FillAsync);

        private async Task FillAsync()
        {
            var userBooks = await _userBookDAL.SelectUserBooksAsync(User.Id, true);

            Books.Clear();
            foreach (var book in userBooks)
                Books.Add(book.Book);
        }
    }
}
