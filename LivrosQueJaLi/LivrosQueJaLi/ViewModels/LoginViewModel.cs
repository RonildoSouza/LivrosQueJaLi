using EnviaEmailDLL.Modelo.Logica;
using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Helpers;
using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Services;
using LivrosQueJaLi.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private User _user;
        private UserDAL _userDAL;
        private FriendDAL _friendDAL;
        private INavigation _navigation;
        private AzureClient<User> _azureClient;

        private string _email = string.Empty;
        public string Email
        {
            get { return _email?.Trim(); }
            set { SetProperty(ref _email, value?.Trim()); }
        }

        private string _password = string.Empty;
        public string Password
        {
            get { return _password?.Trim(); }
            set { SetProperty(ref _password, value?.Trim()); }
        }

        public Command LoginCommand { get; }
        public Command RegisterCommand { get; }
        public Command LoginFBCommand { get; }

        public LoginViewModel(INavigation pNavigation)
        {
            _userDAL = new UserDAL();
            _friendDAL = new FriendDAL();
            _navigation = pNavigation;
            _azureClient = new AzureClient<User>();

            LoginCommand = new Command(ExecuteLoginCommand);
            RegisterCommand = new Command(ExecuteRegisterCommand);
            LoginFBCommand = new Command(ExecuteLoginFBCommand);
        }

        private async void ExecuteLoginCommand()
        {
            try
            {
                if (!string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
                {
                    IsBusy = true;
                    IsVisible = false;
                    _user = await _userDAL.SelectByIdFacebookOrEmailAsync(string.Empty, Email);

                    var password = _user.Password ?? string.Empty;
                    var passwordDecrypt = Encryption.DecryptAes(password);
                    if (_user != null && Password.Equals((passwordDecrypt)) && ValidEmail(Email))
                        await NavigationToMainPage();
                    else
                    {
                        IsBusy = false;
                        IsVisible = true;
                        DisplayAlertShow("Falha no Login", "Email e/ou Senha inválido!");
                    }
                }
                else
                    DisplayAlertShow("Campos Vazios", "Email e/ou Senha não informados!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async void ExecuteRegisterCommand()
        {
            await _navigation.PushAsync(new RegisterPage());
        }

        private async void ExecuteLoginFBCommand()
        {
            try
            {
                IsVisible = false;

                _user = await _azureClient.LoginAsync();

                if (_user != null)
                {
                    IsBusy = true;
                    _user.Photo = $"https://graph.facebook.com/{_user.IdFacebook}/picture?type=normal&hc_location=ufi";
                    var userDB = await _userDAL.SelectByIdFacebookOrEmailAsync(_user.IdFacebook, _user.Email);

                    if (userDB != null && string.IsNullOrEmpty(userDB.IdFacebook))
                    {
                        _user.Id = userDB.Id;
                        _user.Password = userDB.Password;
                        userDB = await InsertOrUpdateAndGetUser(_user, userDB);
                    }
                    else
                        userDB = await InsertOrUpdateAndGetUser(_user, userDB);

                    _user = userDB;
                    await NavigationToMainPage();
                }
                else
                {
                    IsVisible = true;
                    return;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private async Task NavigationToMainPage()
        {
            _user.Friends = await _friendDAL.SelectUserFriendsAsync(_user);
            Constants.User = _user;
            IsBusy = false;

            await _navigation.PushAsync(new MainPage());
            RemovePageFromStack<LoginPage>(_navigation);
        }

        private async Task<User> InsertOrUpdateAndGetUser(User user, User userDB)
        {
            _userDAL.InsertOrUpdate(user);
            userDB = await _userDAL.SelectByIdFacebookOrEmailAsync(user.IdFacebook, user.Email);
            return userDB;
        }

        protected override Task FillObservableCollectionAsync() => throw new NotImplementedException();
    }
}
