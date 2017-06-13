using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class BaseUserBookViewModel : BaseViewModel
    {
        private UserBookDAL _userBookDAL;

        public Command RefreshCommand { get; }

        public Command RemoveUserBookCommand { get; }

        public BaseUserBookViewModel()
        {
            _userBookDAL = new UserBookDAL();
            RefreshCommand = new Command(ExecuteRefreshCommand);
            RemoveUserBookCommand = new Command(ExecuteRemoveUserBookCommand);
        }

        private void ExecuteRefreshCommand() => FillListView(FillObservableCollectionAsync);

        protected override async Task FillObservableCollectionAsync()
        {
            var userBooks = await _userBookDAL.SelectUserBooksAsync(User.Id);

            Books.Clear();
            foreach (var book in userBooks)
                Books.Add(book.Book);
        }

        private async void ExecuteRemoveUserBookCommand(object obj)
        {
            try
            {
                var book = obj as Book;
                Books.Remove(book);
                var userBook = await _userBookDAL.SelectUserBookByIds(User.Id, book.Id);
                _userBookDAL.DeleteUserBook(userBook);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
