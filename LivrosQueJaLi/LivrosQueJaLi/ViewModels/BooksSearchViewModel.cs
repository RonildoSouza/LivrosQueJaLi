using LivrosQueJaLi.Models;
using LivrosQueJaLi.Services;
using LivrosQueJaLi.Views;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class BooksSearchViewModel : BaseViewModel
    {
        private GoogleBooksClient _googleBooksClient;

        public ObservableRangeCollection<Book> Books { get; private set; }

        

        public BooksSearchViewModel()
        {
            _googleBooksClient = new GoogleBooksClient();
            Books = new ObservableRangeCollection<Book>();

            FillListView(FillBooks);
        }

        private async void FillBooks()
        {
            var books = await _googleBooksClient.GetBooksAsync();
            Books.ReplaceRange(books);
        }
    }
}
