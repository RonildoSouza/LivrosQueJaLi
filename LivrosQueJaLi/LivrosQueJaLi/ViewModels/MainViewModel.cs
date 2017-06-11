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
        private INavigation _navigation;

        public Command LogoutCommand { get; }
        public Command FriendsCommand { get; set; }

        public MainViewModel(INavigation pNavigation)
        {
            _navigation = pNavigation;
            LogoutCommand = new Command(ExecuteLogoutCommand);
            FriendsCommand = new Command(ExecuteFriendsCommand);
        }

        private async void ExecuteLogoutCommand()
        {
            await new AzureClient<User>().LogoutAsync();
            App.Current.MainPage = new NavigationPage(new LoginPage());
        }

        private async void ExecuteFriendsCommand(object obj)
        {
            await _navigation.PushAsync(new FriendsPage());
        }

        protected override Task FillObservableCollectionAsync() => throw new NotImplementedException();
    }
}
