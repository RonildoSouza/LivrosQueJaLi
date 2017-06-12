using LivrosQueJaLi.Models;
using LivrosQueJaLi.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class BookDetailTabbedViewModel : BaseViewModel
    {
        private Book _book;
        public Book Book
        {
            get { return _book; }
            set { SetProperty(ref _book, value); }
        }

        public Command CommentCommand { get; set; }

        public BookDetailTabbedViewModel(Book pBook)
        {
            Book = pBook;
            CommentCommand = new Command(ExecuteCommentCommand);
        }

        private void ExecuteCommentCommand(object obj) => NavigationToPush(new CommentPage(Book));

        protected override Task FillObservableCollectionAsync()
        {
            throw new NotImplementedException();
        }
    }
}
