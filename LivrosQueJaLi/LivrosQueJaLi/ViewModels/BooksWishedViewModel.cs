using LivrosQueJaLi.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosQueJaLi.ViewModels
{
    public class BooksWishedViewModel : BookBaseViewModel
    {
        private UserBookDAL _userBookDAL;

        public BooksWishedViewModel()
        {
            _userBookDAL = new UserBookDAL();
            FillListView(FillListViewAction);
        }

        private async void FillListViewAction()
        {
            var userBooks = await _userBookDAL.SelectUserBooksAsync("afb376f1-3198-49d8-83a5-b8a8e86ae741", true);

            Books.Clear();
            foreach (var book in userBooks)
                Books.Add(book.Book);
        }
    }
}
