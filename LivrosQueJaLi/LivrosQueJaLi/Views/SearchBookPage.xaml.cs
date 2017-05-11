using LivrosQueJaLi.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LivrosQueJaLi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchBookPage : ContentPage
    {
        public SearchBookPage()
        {
            InitializeComponent();
            BindingContext = new SearchBookViewModel();
        }
    }
}
