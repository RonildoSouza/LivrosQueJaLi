using LivrosQueJaLi.Models;
using LivrosQueJaLi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class BooksSearchViewModel : BaseViewModel
    {
        private GoogleBooksClient _googleBooksClient;

        public Command SearchCommand { get; }

        private string _textSearch;
        public string TextSearch
        {
            get { return _textSearch; }
            set { SetProperty(ref _textSearch, value); }
        }

        public BooksSearchViewModel()
        {
            _googleBooksClient = new GoogleBooksClient();
            SearchCommand = new Command(ExecuteSearchCommand);

            ExecuteSearchCommand();
            IsBusy = false;
        }

        private void ExecuteSearchCommand() => FillListView(FillObservableCollectionAsync);

        protected override async Task FillObservableCollectionAsync()
        {
            List<Book> books;

            if (string.IsNullOrEmpty(TextSearch))
                books = await _googleBooksClient.GetBooksAsync();
            else
                books = await _googleBooksClient.GetBooksAsync(TextSearch);

            Books.ReplaceRange(books);
        }
    }
}