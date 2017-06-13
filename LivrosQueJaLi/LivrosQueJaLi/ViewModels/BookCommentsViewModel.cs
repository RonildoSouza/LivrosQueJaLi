using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using MvvmHelpers;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class BookCommentsViewModel : BaseViewModel
    {
        private Book _book;
        private CommentDAL _commentDAL;

        public ObservableRangeCollection<Comment> Comments { get; set; }

        public Command RefreshCommand { get; }
        public Command CommentCommand { get; set; }

        public BookCommentsViewModel(Book pBook)
        {
            _book = pBook;
            _commentDAL = new CommentDAL();
            Comments = new ObservableRangeCollection<Comment>();

            RefreshCommand = new Command(ExecuteRefreshCommand);
            CommentCommand = new Command(ExecuteCommentCommand);
        }

        private void ExecuteCommentCommand(object obj)
        {
            var comment = obj as Comment;
            DisplayAlertShow(comment?.CreatedAt.ToString(), comment?.CommentText);
        }

        private void ExecuteRefreshCommand() => FillListView(FillObservableCollectionAsync);

        protected override async Task FillObservableCollectionAsync()
        {
            var comments = await _commentDAL.SelectBookCommentsAsync(_book.Id);
            Comments.ReplaceRange(comments);
        }
    }
}
