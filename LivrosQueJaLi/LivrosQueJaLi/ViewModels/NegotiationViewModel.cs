using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using MvvmHelpers;
using System.Threading.Tasks;
using Xamarin.Forms;
using System;
using LivrosQueJaLi.Helpers;

namespace LivrosQueJaLi.ViewModels
{
    public class NegotiationViewModel : BaseViewModel
    {
        private User _user;
        private Book _book;
        private NegotiationDAL _negotiationDAL;
        private UserBookDAL _userBookDAL;

        public ObservableRangeCollection<Negotiation> Negotiations { get; set; }

        public string Title { get; }

        private string _message = string.Empty;
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        public Command RefreshNegotiationsCommand { get; }
        public Command SendMessageCommand { get; }

        public NegotiationViewModel(User pUser, Book pBook)
        {
            Title = $"Negociação - {pBook.VolumeInfo.Title}";

            _user = pUser;
            _book = pBook;

            _negotiationDAL = new NegotiationDAL();
            _userBookDAL = new UserBookDAL();

            Negotiations = new ObservableRangeCollection<Negotiation>();

            RefreshNegotiationsCommand = new Command(ExecuteRefreshNegotiationsCommand);
            SendMessageCommand = new Command(ExecuteSendMessageCommand);
        }

        private async void ExecuteSendMessageCommand(object obj)
        {
            try
            {
                var userBook = await _userBookDAL.SelectUserBookByIds(_user.Id, _book.Id);
                _negotiationDAL.InsertOrUpdate(new Negotiation()
                {
                    IdUserBook = userBook.Id,
                    IdUserInterested = User.Id,
                    Message = Message
                });

                Message = string.Empty;
                RefreshNegotiationsCommand.Execute(null);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ExecuteRefreshNegotiationsCommand(object obj)
        {
            FillListView(FillObservableCollectionAsync);
        }

        protected async override Task FillObservableCollectionAsync()
        {
            var ngts = await _negotiationDAL.SelectNegotiations(_user.Id, _book.Id);

            if (ngts != null)
            {
                foreach (var ngt in ngts)
                    ngt.UserAndDate = $"{User.UserName} - {ngt.CreatedAt}";

                Negotiations.ReplaceRange(ngts);
            }
        }
    }
}
