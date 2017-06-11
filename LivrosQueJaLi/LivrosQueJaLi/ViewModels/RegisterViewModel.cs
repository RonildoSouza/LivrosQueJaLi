using EnviaEmailDLL.Modelo.Logica;
using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Helpers;
using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private UserDAL _userDAL;
        private FriendDAL _friendDAL;
        private INavigation _navigation;

        private string _name = string.Empty;
        public string Name
        {
            get { return _name?.Trim(); }
            set { SetProperty(ref _name, value?.Trim()); }
        }

        private string _email = string.Empty;
        public string Email
        {
            get { return _email?.Trim(); }
            set { SetProperty(ref _email, value.Trim()); }
        }

        private string _password = string.Empty;
        public string Password
        {
            get { return _password?.Trim(); }
            set { SetProperty(ref _password, value?.Trim()); }
        }

        private string _confirmPassword = string.Empty;
        public string ConfirmPassword
        {
            get { return _confirmPassword?.Trim(); }
            set { SetProperty(ref _confirmPassword, value?.Trim()); }
        }

        public Command RegisterCommand { get; }

        public RegisterViewModel(INavigation pNavigation)
        {
            _navigation = pNavigation;
            _userDAL = new UserDAL();
            _friendDAL = new FriendDAL();
            RegisterCommand = new Command(ExecuteRegisterCommand);
        }

        private async void ExecuteRegisterCommand(object obj)
        {
            try
            {
                if (ValidEmail(Email) && ValidPassword(Password, ConfirmPassword))
                {
                    IsBusy = true;
                    IsVisible = false;
                    var user = new User()
                    {
                        UserName = Name,
                        Email = this.Email,
                        Password = Encryption.EncryptAes(this.Password),
                        Friends = new List<User>()
                    };

                    var usr = await _userDAL.SelectByIdFacebookOrEmailAsync(string.Empty, Email);
                    if (usr != null)
                    {
                        usr.Password = user.Password;
                        user = usr;
                        user.Friends = await _friendDAL.SelectUserFriendsAsync(user);
                        usr = null;
                    }

                    _userDAL.InsertOrUpdate(user);

                    Constants.User = await _userDAL.SelectByIdFacebookOrEmailAsync(string.Empty, Email);
                    IsBusy = false;

                    await _navigation.PushAsync(new MainPage());
                    RemovePageFromStack<RegisterPage>(_navigation);
                }
                else
                    DisplayAlertShow("Verifique", "\n\t* Se o Email está correto" +
                        "\n\t* Ou informe as Senhas novamente pois podem estar diferentes!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected override Task FillObservableCollectionAsync() => throw new NotImplementedException();
    }
}
