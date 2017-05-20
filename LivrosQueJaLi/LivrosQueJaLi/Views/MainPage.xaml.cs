using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LivrosQueJaLi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage
    {
        public MainPage(User pUser)
        {
            InitializeComponent();
            BindingContext = new MainViewModel(pUser);
        }
    }
}
