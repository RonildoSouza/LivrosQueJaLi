using LivrosQueJaLi.Models;
using LivrosQueJaLi.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LivrosQueJaLi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WhoReadPage : ContentPage
    {
        public WhoReadPage(Book pBook)
        {
            InitializeComponent();
            BindingContext = new WhoReadViewModel(pBook);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as WhoReadViewModel)?.RefreshUsersCommand.Execute(null);
        }
    }
}