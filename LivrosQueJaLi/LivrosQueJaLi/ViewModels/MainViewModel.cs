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

        public MainViewModel(User pUser)
        {
            LogoutCommand = new Command(ExecuteLogoutCommand);
        }

        private async void ExecuteLogoutCommand(object obj)
        {
            await new AzureClient<User>().LogoutAsync();

            App.Current.MainPage = new NavigationPage(new LoginPage());
        }

        protected override Task FillObservableCollectionAsync() => throw new NotImplementedException();
    }
}
