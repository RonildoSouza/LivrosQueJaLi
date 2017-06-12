using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models.Entities;
using MvvmHelpers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class FriendsViewModel : BaseViewModel
    {
        private UserDAL _userDAL;
        private FriendDAL _friendDAL;

        public ObservableRangeCollection<User> Friends { get; set; }

        public Command SearchCommand { get; }
        public Command AddFriendCommand { get; }
        public Command RemoveFriendCommand { get; }

        private string _textSearch;
        public string TextSearch
        {
            get { return _textSearch; }
            set { SetProperty(ref _textSearch, value); }
        }

        public FriendsViewModel()
        {
            _userDAL = new UserDAL();
            _friendDAL = new FriendDAL();
            Friends = new ObservableRangeCollection<User>();
            SearchCommand = new Command(ExecuteSearchCommand);
            AddFriendCommand = new Command(ExecuteAddFriendCommand);
            RemoveFriendCommand = new Command(ExecuteRemoveFriendCommand);

            if (User?.Friends != null && User?.Friends.Count > 0)
                Friends.ReplaceRange(User.Friends);
        }

        private void ExecuteSearchCommand(object obj) => FillListView(FillObservableCollectionAsync);

        private void ExecuteAddFriendCommand(object obj)
        {
            var friend = obj as User;

            if (!Friends.Contains(friend))
                _friendDAL.InsertOrUpdate(new Friend() { IdUser = User.Id, IdUserFriend = friend.Id });
            else
                DisplayAlertShow("Sou seu Amigo!", $"O usuário {friend.UserName} já é seu amigo!");
        }

        private void ExecuteRemoveFriendCommand(object obj)
        {
            var friend = obj as User;

            if (Friends.Contains(friend))
            {
                _friendDAL.Delete(new Friend() { IdUser = User.Id, IdUserFriend = friend.Id });
                User.Friends.Remove(friend);
                Friends.Remove(friend);
            }
            else
                DisplayAlertShow("Não sou seu Amigo!", $"Você não pode remover o usuário {friend.UserName}" +
                    $"\nporque ele não está na sua lista de amigos!");
        }

        protected async override Task FillObservableCollectionAsync()
        {
            User user = new User();

            if (string.IsNullOrEmpty(TextSearch) || TextSearch.ToLower().Equals(User.Email))
                ReplaceCollectionFriends();
            else
                user = await _userDAL.SelectByIdFacebookOrEmailAsync(string.Empty, TextSearch);

            Friends.Replace(user);
        }

        private async void ReplaceCollectionFriends() =>
            Friends.ReplaceRange(await _friendDAL.SelectUserFriendsAsync(User));
    }
}
