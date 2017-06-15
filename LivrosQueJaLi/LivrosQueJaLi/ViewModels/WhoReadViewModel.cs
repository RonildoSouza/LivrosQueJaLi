using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Views;
using MvvmHelpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class WhoReadViewModel : BaseViewModel
    {
        private Book _book;
        private UserDAL _userDAL;

        public ObservableRangeCollection<User> Users { get; set; }

        public Command RefreshUsersCommand { get; }
        public Command UserDetailCommand { get; }

        public WhoReadViewModel(Book pBook)
        {
            _book = pBook;
            _userDAL = new UserDAL();
            Users = new ObservableRangeCollection<User>();

            RefreshUsersCommand = new Command(ExecuteRefreshUsersCommand);
            UserDetailCommand = new Command(ExecuteUserDetailCommand);
        }

        private void ExecuteUserDetailCommand(object obj)
        {
            if (obj == null)
                return;

            var user = obj as User;

            if (string.IsNullOrEmpty(user.LentOrSeeling))
                DisplayAlertShow("Ops!", "Não empresto, não vendo\nou está emprestado ou já vendi!");
            else
                NavigationToPush(new NegotiationPage(user, _book));
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

                if (dyn.UserBook.Lent)
                    dyn.User.LentOrSeeling = "Empresto este livro.";

                if (dyn.UserBook.Seeling)
                    dyn.User.LentOrSeeling = "Vendo este livro.";
            }

            if (users != null)
                Users.ReplaceRange(users);
        }
    }
}
