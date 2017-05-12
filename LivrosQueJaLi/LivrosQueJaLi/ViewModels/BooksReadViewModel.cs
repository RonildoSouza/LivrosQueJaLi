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

namespace LivrosQueJaLi.ViewModels
{
    public class BooksReadViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Book> Books { get; private set; }
        private UserBookDAL _userBookDAL;

        public BooksReadViewModel()
        {
            Books = new ObservableRangeCollection<Book>();
            _userBookDAL = new UserBookDAL();

            FillListView(FillListViewAction);
        }

        private async void FillListViewAction()
        {
            var userBooks = await _userBookDAL.SelectUserBooksAsync(User.Id);

            Books.Clear();
            foreach (var book in userBooks)
                Books.Add(book.Book);
        }
    }
}
