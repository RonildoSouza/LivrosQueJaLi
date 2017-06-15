using LivrosQueJaLi.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LivrosQueJaLi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InterestedUsersPage : ContentPage
    {
        public InterestedUsersPage()
        {
            InitializeComponent();
            BindingContext = new InterestedUsersViewModel();
        }
    }
}