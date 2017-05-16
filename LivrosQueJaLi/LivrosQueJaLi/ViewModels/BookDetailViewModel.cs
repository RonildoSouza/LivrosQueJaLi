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
    public class BookDetailViewModel : BaseViewModel
    {
        private UserBookDAL _userBookDAL;

        private Book _book;
        public Book Book
        {
            get { return _book; }
            set { SetProperty(ref _book, value); }
        }

        private string _authors;
        public string Authors
        {
            get { return _authors; }
            set { SetProperty(ref _authors, value); }
        }

        public Command ReadCommand { get; set; }

        public Command WishCommand { get; set; }

        public BookDetailViewModel(Book pBook)
        {
            _userBookDAL = new UserBookDAL();
            Book = pBook;
            ReadCommand = new Command(ExecuteReadCommand);
            WishCommand = new Command(ExecuteWishCommand);

            FillAuthors();
        }

        private async void ExecuteReadCommand(object obj)
        {
            try
            {
                var userBook = await _userBookDAL.SelectUserBookByIds(User.Id, Book.Id);

                if (userBook == null)
                    userBook = GetInstance(true, false);
                else
                {
                    userBook.IsRead = true;
                    userBook.IsWish = false;
                }

                _userBookDAL.InsertOrUpdate(userBook);
                await App.Current.MainPage.DisplayAlert("Lido", "Add aos livros lidos", "OK");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async void ExecuteWishCommand(object obj)
        {
            try
            {
                var userBook = await _userBookDAL.SelectUserBookByIds(User.Id, Book.Id);

                if (userBook == null)
                    userBook = GetInstance(false, true);
                else
                {
                    userBook.IsRead = false;
                    userBook.IsWish = true;
                }

                _userBookDAL.InsertOrUpdate(userBook);
                await App.Current.MainPage.DisplayAlert("Desejado", "Add aos livros desejados", "OK");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FillAuthors()
        {
            for (int i = 0; i < Book.VolumeInfo.Authors.Length; i++)
                Authors +=
                    (Book.VolumeInfo.Authors.Length - i > 1)
                    ? $"{Book.VolumeInfo.Authors[i]}, " : Book.VolumeInfo.Authors[i];
        }

        private UserBook GetInstance(bool isRead, bool isWish)
        {
            return new UserBook()
            {
                IdUser = User.Id,
                IdBook = Book.Id,
                IsRead = isRead,
                IsWish = isWish
            };
        }
    }
}
