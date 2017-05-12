using LivrosQueJaLi.Models;
using LivrosQueJaLi.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosQueJaLi.ViewModels
{
    public class BookSearchViewModel : BaseViewModel
    {
        private GoogleBooksClient _googleBooksClient;
        public ObservableRangeCollection<Book> Books { get; private set; }

        public BookSearchViewModel()
        {
            _googleBooksClient = new GoogleBooksClient();
            Books = new ObservableRangeCollection<Book>();

            FillListView(FillListViewAction);
        }

        private async void FillListViewAction()
        {
            var books = await _googleBooksClient.GetBooksAsync();
            Books.ReplaceRange(books);
        }
    }
}
