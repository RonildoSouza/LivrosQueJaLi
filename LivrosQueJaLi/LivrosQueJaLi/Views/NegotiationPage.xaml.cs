using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LivrosQueJaLi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NegotiationPage : ContentPage
    {
        public NegotiationPage(User pUser, Book pBook)
        {
            InitializeComponent();
            BindingContext = new NegotiationViewModel(pUser, pBook);
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