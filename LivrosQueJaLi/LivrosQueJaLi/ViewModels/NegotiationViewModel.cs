using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using MvvmHelpers;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class NegotiationViewModel : BaseViewModel
    {
        private string _idUserNegotiator;
        private string _idUserInterested;
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

        public NegotiationViewModel(string pIdUserNegotiator, Book pBook, string pIdUserInterested)
        {
            Title = $"Negociação - [{pBook.VolumeInfo.Title}]";

            _idUserNegotiator = pIdUserNegotiator;
            _book = pBook;
            _idUserInterested = pIdUserInterested;

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
                var userBook = await _userBookDAL.SelectUserBookByIds(_idUserNegotiator, _book.Id);
                _negotiationDAL.InsertOrUpdate(new Negotiation()
                {
                    IdUserBook = userBook.Id,
                    IdUserInterested = _idUserInterested,
                    Message = $"{User.UserName} -> {Message}"
                });

                Message = string.Empty;
                RefreshNegotiationsCommand.Execute(null);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void ExecuteRefreshNegotiationsCommand(object obj) =>
            FillListView(FillObservableCollectionAsync);

        protected async override Task FillObservableCollectionAsync()
        {
            var ngts = await _negotiationDAL.SelectNegotiations(_idUserNegotiator, _book.Id, _idUserInterested);

            if (ngts != null)
                Negotiations.ReplaceRange(ngts);
        }
    }
}
