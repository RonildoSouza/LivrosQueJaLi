using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Views;
using MvvmHelpers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class InterestedUsersViewModel : BaseViewModel
    {
        private NegotiationDAL _negotiationDAL;
        private UserBookDAL _userBookDAL;
        private UserDAL _userDAL;
        private Book _book;

        public ObservableRangeCollection<User> InterestedUsers { get; set; }

        public string Title { get; }

        public Command RefreshInterestedUsersCommand { get; }
        public Command InterestedUserDetailCommand { get; }
        public Command DeleteCommand { get; }

        public InterestedUsersViewModel(Book pBook)
        {
            _negotiationDAL = new NegotiationDAL();
            _userBookDAL = new UserBookDAL();
            _userDAL = new UserDAL();
            _book = pBook;

            Title = $"Usuários Interessados - [{_book.VolumeInfo.Title}]";

            InterestedUsers = new ObservableRangeCollection<User>();

            RefreshInterestedUsersCommand = new Command(ExecuteRefreshInterestedUsersCommand);
            InterestedUserDetailCommand = new Command(ExecuteInterestedUserDetailCommand);
            DeleteCommand = new Command(ExecuteDeleteCommand);
        }

        private async void ExecuteDeleteCommand(object obj)
        {
            if (obj == null)
                return;

            var userInterested = obj as User;
            var userNegotiations = await _negotiationDAL.SelectNegotiations(User.Id, _book.Id, userInterested.Id);

            foreach (var userNeg in userNegotiations)
                _negotiationDAL.Delete(userNeg);

            ExecuteRefreshInterestedUsersCommand();
        }

        private void ExecuteRefreshInterestedUsersCommand() =>
            FillListView(FillObservableCollectionAsync);

        private void ExecuteInterestedUserDetailCommand(object obj)
        {
            if (obj == null)
                return;

            var user = obj as User;
            NavigationToPush(new NegotiationPage(User.Id, _book, user.Id));
        }

        protected async override Task FillObservableCollectionAsync()
        {
            var userBook = await _userBookDAL.SelectUserBookByIds(User.Id, _book.Id);
            var users = await _userDAL.SelectInterestedUsersBookAsync(userBook);

            if (users != null)
            {
                InterestedUsers.Clear();
                foreach (var u in users)
                    InterestedUsers.Add(u.User);
            }
        }
    }
}
