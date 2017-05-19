using System;
using System.Threading.Tasks;
using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Helpers;
using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Services;
using LivrosQueJaLi.Views;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private UserDAL _userDAL;
        private AzureClient<User> _azureClient;

        public Command LoginFBCommand { get; set; }

        public LoginViewModel()
        {
            _userDAL = new UserDAL();
            _azureClient = new AzureClient<User>();

            LoginFBCommand = new Command(ExecuteLoginFBCommand);
        }

        private async void ExecuteLoginFBCommand()
        {
            var user = await _azureClient.LoginAsync();

            if (user != null)
            {
                var userDB = await _userDAL.SelectByIdFacebookAsync(user.IdFacebook);

                if (userDB == null)
                {
                    _userDAL.InsertOrUpdate(user);
                    userDB = await _userDAL.SelectByIdFacebookAsync(user.IdFacebook);
                }

                user = userDB;
                Constants.User = user;

                await Application.Current.MainPage.Navigation.PushAsync(new MainPage(user));
            }
        }

        protected override Task FillObservableCollectionAsync()
        {
            throw new NotImplementedException();
        }
    }
}
