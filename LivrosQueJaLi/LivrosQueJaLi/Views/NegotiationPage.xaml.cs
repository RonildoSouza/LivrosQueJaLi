using LivrosQueJaLi.Models;
using LivrosQueJaLi.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LivrosQueJaLi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NegotiationPage : ContentPage
    {
        public NegotiationPage(string pUserNegotiator, Book pBook, string pIdUserInterested)
        {
            InitializeComponent();
            BindingContext = new NegotiationViewModel(pUserNegotiator, pBook, pIdUserInterested);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext == null)
                return;

            (BindingContext as NegotiationViewModel).RefreshNegotiationsCommand.Execute(null);
        }
    }
}