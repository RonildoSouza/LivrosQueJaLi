using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using System;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace LivrosQueJaLi.ViewModels
{
    public class CommentViewModel : BaseViewModel
    {
        private Book _book;
        private CommentDAL _commentDAL;

        private string _editorText;
        public string EditorText
        {
            get { return _editorText; }
            set { SetProperty(ref _editorText, value); }
        }

        public Command SendCommand { get; set; }

        public CommentViewModel(Book pBook)
        {
            _book = pBook;
            _commentDAL = new CommentDAL();
            SendCommand = new Command(ExecuteSendCommand);
        }

        private async void ExecuteSendCommand(object obj)
        {
            try
            {
                var comment = new Comment()
                {
                    IdUserComment = User.Id,
                    IdBook = _book.Id,
                    CommentText = EditorText
                };

                _commentDAL.InsertOrUpdate(comment);

                DisplayAlertShow("Enviado", "Comentário enviado com sucesso!");

                await App.Current.MainPage.Navigation.PopAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override Task FillObservableCollectionAsync() => throw new NotImplementedException();
    }
}
