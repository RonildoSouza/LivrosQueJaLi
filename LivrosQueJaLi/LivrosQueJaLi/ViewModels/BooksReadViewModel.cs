using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using MvvmHelpers;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class BooksReadViewModel : BaseViewModel
    {
        private UserBookDAL _userBookDAL;

        public ObservableRangeCollection<Book> Books { get; private set; }

        public Command RefreshCommand { get; }

        public Command RemoveUserBookCommand { get; }

        public BooksReadViewModel()
        {
            _userBookDAL = new UserBookDAL();
            Books = new ObservableRangeCollection<Book>();

            RefreshCommand = new Command(ExecuteRefreshCommand);
            RemoveUserBookCommand = new Command(ExecuteRemoveUserBookCommand);
        }

        private async void ExecuteRemoveUserBookCommand(object obj)
        {
            try
            {
                var book = obj as Book;
                Books.Remove(book);
                var userBookDAL = new UserBookDAL();
                var userBook = await userBookDAL.SelectUserBookByIds(User.Id, book.Id);
                userBookDAL.DeleteUserBook(userBook);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ExecuteRefreshCommand() => FillListView(FillAsync);

        private async Task FillAsync()
        {
            var userBooks = await _userBookDAL.SelectUserBooksAsync(User.Id);

            Books.Clear();
            foreach (var book in userBooks)
                Books.Add(book.Book);
        }
    }
}
