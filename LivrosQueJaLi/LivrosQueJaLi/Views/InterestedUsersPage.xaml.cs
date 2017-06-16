using LivrosQueJaLi.Models;
using LivrosQueJaLi.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LivrosQueJaLi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InterestedUsersPage : ContentPage
    {
        public InterestedUsersPage(Book pBook)
        {
            InitializeComponent();
            BindingContext = new InterestedUsersViewModel(pBook);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext == null)
                return;

            (BindingContext as InterestedUsersViewModel).RefreshInterestedUsersCommand.Execute(null);
        }
    }
}