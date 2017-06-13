using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using MvvmHelpers;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using System.Collections.Generic;

namespace LivrosQueJaLi.ViewModels
{
    public class WhoReadViewModel : BaseViewModel
    {
        private Book _book;
        private UserDAL _userDAL;

        public ObservableRangeCollection<User> Users { get; set; }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }

        private string _lentOrSeeling;
        public string LentOrSeeling
        {
            get { return _lentOrSeeling; }
            set { SetProperty(ref _lentOrSeeling, value); }
        }

        public Command RefreshUsersCommand { get; set; }

        public WhoReadViewModel(Book pBook)
        {
            _book = pBook;
            _userDAL = new UserDAL();
            Users = new ObservableRangeCollection<User>();

            RefreshUsersCommand = new Command(ExecuteRefreshUsersCommand);
        }

        private void ExecuteRefreshUsersCommand() =>
            FillListView(FillObservableCollectionAsync);

        protected async override Task FillObservableCollectionAsync()
        {
            var usersDynamic = await _userDAL.SelectUsersWhoReadBookAsync(_book);
            var users = new List<User>();

            foreach (var dyn in usersDynamic)
            {
                users.Add(dyn.User);
                IsEnabled = dyn.UserBook.Lent || dyn.UserBook.Seeling;

                if (dyn.UserBook.Lent)
                    LentOrSeeling = "Empresto este livro.";

                if (dyn.UserBook.Seeling)
                    LentOrSeeling = "Vendo este livro.";
            }

            if (users != null)
                Users.ReplaceRange(users);
        }
    }
}
