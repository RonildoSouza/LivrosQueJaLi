using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LivrosQueJaLi.ViewModels
{
    public class BookCommentsViewModel : BaseViewModel
    {
        private Book _book;
        private CommentDAL _commentDAL;

        public ObservableRangeCollection<Comment> Comments { get; set; }    

        public BookCommentsViewModel(Book pBook)
        {
            _book = pBook;
            _commentDAL = new CommentDAL();
            Comments = new ObservableRangeCollection<Comment>();
            FillListView(FillComments);
        }

        private async void FillComments()
        {
            var comments = await _commentDAL.SelectBookCommentsAsync(_book.Id);
            Comments.ReplaceRange(comments);
        }
    }
}
