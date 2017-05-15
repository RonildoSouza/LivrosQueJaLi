using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            FillListView(FillBooks);
        }

        private async void FillBooks()
        {
            var userBooks = await _userBookDAL.SelectUserBooksAsync(User.Id, true);

            Books.Clear();
            foreach (var book in userBooks)
                Books.Add(book.Book);
        }
    }
}
