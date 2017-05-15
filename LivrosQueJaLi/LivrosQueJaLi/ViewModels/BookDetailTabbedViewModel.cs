using LivrosQueJaLi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public BookDetailTabbedViewModel(Book pBook)
        {
            Book = pBook;
        }
    }
}
