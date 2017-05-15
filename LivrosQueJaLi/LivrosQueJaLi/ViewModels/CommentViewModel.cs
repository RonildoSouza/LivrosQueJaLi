using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

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

        private void ExecuteSendCommand(object obj)
        {
            var comment = new Comment()
            {
                UserComment = User.UserName,
                IdBook = _book.Id,
                CommentText = EditorText
            };

            try
            {
                _commentDAL.InsertOrUpdate(comment);

                App.Current.MainPage.DisplayAlert(
                    "Sucesso", "Comentário enviado com sucesso", "OK");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
