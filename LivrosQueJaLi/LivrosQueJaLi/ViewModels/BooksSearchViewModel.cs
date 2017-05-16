using LivrosQueJaLi.Models;
using LivrosQueJaLi.Services;
using MvvmHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class BooksSearchViewModel : BaseViewModel
    {
        private GoogleBooksClient _googleBooksClient;

        public ObservableRangeCollection<Book> Books { get; private set; }

        public Command SearchCommand { get; set; }

        private string _textSearch;
        public string TextSearch
        {
            get { return _textSearch; }
            set { SetProperty(ref _textSearch, value); }
        }

        public BooksSearchViewModel()
        {
            _googleBooksClient = new GoogleBooksClient();
            Books = new ObservableRangeCollection<Book>();
            SearchCommand = new Command(ExecuteSearchCommand);

            FillBooks();
        }

        private void ExecuteSearchCommand() => FillBooks();

        private async void FillBooks() => await FillListView(FillAsync);

        private async Task FillAsync()
        {
            IEnumerable<Book> books;

            if (string.IsNullOrEmpty(TextSearch))
                books = await _googleBooksClient.GetBooksAsync();
            else
                books = await _googleBooksClient.GetBooksAsync(TextSearch);

            Books.ReplaceRange(books);
        }
    }
}
