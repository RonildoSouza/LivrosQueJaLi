using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Services;
using LivrosQueJaLi.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public Command LogoutCommand { get; }

        public MainViewModel()
        {
            LogoutCommand = new Command(ExecuteLogoutCommand);
        }

        private async void ExecuteLogoutCommand()
        {
            await new AzureClient<User>().LogoutAsync();
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }

        protected override Task FillObservableCollectionAsync() => throw new NotImplementedException();
    }
}
