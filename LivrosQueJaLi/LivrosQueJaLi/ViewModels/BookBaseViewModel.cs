using LivrosQueJaLi.Models;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosQueJaLi.ViewModels
{
    public class BookBaseViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Book> Books { get; private set; }

        public BookBaseViewModel()
        {
            Books = new ObservableRangeCollection<Book>();
        }
    }
}
