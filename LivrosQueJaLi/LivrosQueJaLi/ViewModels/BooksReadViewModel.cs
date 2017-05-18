using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class BooksReadViewModel : BaseViewModel
    {
        private UserBookDAL _userBookDAL;

        public ObservableRangeCollection<Book> Books { get; private set; }

        public Command RefreshCommand { get; }

        public BooksReadViewModel()
        {
            _userBookDAL = new UserBookDAL();
            Books = new ObservableRangeCollection<Book>();

            RefreshCommand = new Command(ExecuteRefreshCommand);
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
