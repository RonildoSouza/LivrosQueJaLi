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
    public partial class BookCommentsPage : ContentPage
    {
        public BookCommentsPage(Book pBook)
        {
            InitializeComponent();
            BindingContext = new BookCommentsViewModel(pBook);
        }
    }
}