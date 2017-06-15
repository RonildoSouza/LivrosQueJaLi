using LivrosQueJaLi.Models;
using LivrosQueJaLi.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LivrosQueJaLi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetailPage : ContentPage
    {
        public BookDetailPage(Book pBook, bool pIsVisible = false)
        {
            InitializeComponent();
            BindingContext = new BookDetailViewModel(pBook, pIsVisible);
        }
    }   
}