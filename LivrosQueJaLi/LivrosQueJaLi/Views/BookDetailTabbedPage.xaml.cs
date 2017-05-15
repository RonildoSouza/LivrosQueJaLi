using LivrosQueJaLi.Models;
using LivrosQueJaLi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LivrosQueJaLi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetailTabbedPage : TabbedPage
    {
        public BookDetailTabbedPage(Book pBook)
        {
            InitializeComponent();
            BindingContext = new BookDetailTabbedViewModel(pBook);

            Children.Add(new BookDetailPage(pBook));
            Children.Add(new BookCommentsPage(pBook));
        }
    }
}