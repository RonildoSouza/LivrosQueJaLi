using LivrosQueJaLi.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LivrosQueJaLi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BooksReadPage : ContentPage
    {
        public BooksReadPage()
        {
            InitializeComponent();
            BindingContext = new BooksReadViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as BooksReadViewModel)?.FillBooksAsync();
        }
    }
}