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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as BooksReadViewModel)?.RefreshCommand.Execute(null);
        }
    }
}