using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosQueJaLi.ViewModels
{
    public class BooksReadViewModel : BookBaseViewModel
    {
        private GoogleBooksClient _googleBooksClient;
        private UserBookDAL _userBookDAL;

        public BooksReadViewModel()
        {
            _googleBooksClient = new GoogleBooksClient();

            _userBookDAL = new UserBookDAL();
            FillListView(FillListViewAction);
        }

        private async void FillListViewAction()
        {
            //var books = await _googleBooksClient.GetBooksAsync();
            var userBooks = await _userBookDAL.SelectUserBooksAsync("afb376f1-3198-49d8-83a5-b8a8e86ae741");

            Books.Clear();
            foreach (var book in userBooks)
                Books.Add(book.Book);
        }
    }
}
