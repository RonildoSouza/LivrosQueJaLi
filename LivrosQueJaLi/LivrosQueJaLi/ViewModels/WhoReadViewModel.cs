using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using MvvmHelpers;
using System.Threading.Tasks;

namespace LivrosQueJaLi.ViewModels
{
    public class WhoReadViewModel : BaseViewModel
    {
        private Book _book;
        private UserDAL _userDAL;
        public ObservableRangeCollection<User> Users { get; set; }

        public WhoReadViewModel(Book pBook)
        {
            _book = pBook;
            _userDAL = new UserDAL();
            Users = new ObservableRangeCollection<User>();

            FillListView(FillObservableCollectionAsync);
        }

        protected async override Task FillObservableCollectionAsync()
        {
            var users = await _userDAL.SelectUsersWhoReadBookAsync(_book);

            if (users != null)
                Users.ReplaceRange(users);
        }
    }
}
