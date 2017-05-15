using LivrosQueJaLi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosQueJaLi.ViewModels
{
    public class BookDetailViewModel : BaseViewModel
    {
        private Book _book;
        public Book Book
        {
            get { return _book; }
            set { SetProperty(ref _book, value); }
        }

        private string _authors; 
        public string Authors
        {
            get { return _authors; }
            set { SetProperty(ref _authors, value); }
        }

        public BookDetailViewModel(Book pBook)
        {
            Book = pBook;
            FillAuthors();
        }

        private void FillAuthors()
        {
            for (int i = 0; i < Book.VolumeInfo.Authors.Length; i++)
                Authors +=
                    (Book.VolumeInfo.Authors.Length - i > 1)
                    ? $"{Book.VolumeInfo.Authors[i]}, " : Book.VolumeInfo.Authors[i];
        }
    }
}
