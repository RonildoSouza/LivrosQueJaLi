using LivrosQueJaLi.Models;
using LivrosQueJaLi.ViewModels;

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

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as BookCommentsViewModel)?.FillCommentsAsync();
        }
    }
}