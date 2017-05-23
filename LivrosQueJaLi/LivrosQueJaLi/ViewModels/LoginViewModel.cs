using System;
using System.Threading.Tasks;
using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Helpers;
using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Services;
using LivrosQueJaLi.Views;
using Xamarin.Forms;
using System.Linq;

namespace LivrosQueJaLi.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private UserDAL _userDAL;
        private INavigation _navigation;
        private AzureClient<User> _azureClient;

        private bool _isVisible = true;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value); }
        }

        public Command LoginFBCommand { get; set; }

        public LoginViewModel(INavigation pNavigation)
        {
            _userDAL = new UserDAL();
            _navigation = pNavigation;
            _azureClient = new AzureClient<User>();

            LoginFBCommand = new Command(ExecuteLoginFBCommand);
        }

        private async void ExecuteLoginFBCommand()
        {
            try
            {
                IsVisible = false;

                var user = await _azureClient.LoginAsync();

                if (user != null)
                {
                    IsBusy = true;
                    var userDB = await _userDAL.SelectByIdFacebookAsync(user.IdFacebook);

                    if (userDB == null)
                    {
                        _userDAL.InsertOrUpdate(user);
                        userDB = await _userDAL.SelectByIdFacebookAsync(user.IdFacebook);
                    }

                    user = userDB;
                    Constants.User = user;
                    IsBusy = false;

                    await _navigation.PushAsync(new MainPage());
                    RemovePageFromStack();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void RemovePageFromStack()
        {
            var existingPages = _navigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page.GetType() == typeof(LoginPage))
                    _navigation.RemovePage(page);
            }
        }

        protected override Task FillObservableCollectionAsync() => throw new NotImplementedException();
    }
}
