using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using System;
using Xamarin.Forms;
using System.Threading.Tasks;

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

        private string _authors = string.Empty;
        public string Authors
        {
            get { return _authors; }
            set { SetProperty(ref _authors, value); }
        }

        public Command ReadCommand { get; }

        public Command WishCommand { get; }

        public BookDetailViewModel(Book pBook)
        {
            Book = pBook;
            _userBookDAL = new UserBookDAL();
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
                    userBook = GetInstanceUserBook(true, false);
                else
                {
                    userBook.IsRead = true;
                    userBook.IsWish = false;
                }

                _userBookDAL.InsertOrUpdate(userBook);
                DisplayAlertShow("Lido", $"Livro [{Book.VolumeInfo.Title}] adicionado a lista de lidos!");
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
                    userBook = GetInstanceUserBook(false, true);
                else
                {
                    userBook.IsRead = false;
                    userBook.IsWish = true;
                }

                _userBookDAL.InsertOrUpdate(userBook);
                DisplayAlertShow("Desejado", $"Livro [{Book.VolumeInfo.Title}] adicionado a lista de desejados!");
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FillAuthors()
        {
            if (Book.VolumeInfo.Authors != null)
            {
                for (int i = 0; i < Book.VolumeInfo.Authors.Length; i++)
                    Authors +=
                        (Book.VolumeInfo.Authors.Length - i > 1)
                        ? $"{Book.VolumeInfo.Authors[i]}, " : Book.VolumeInfo.Authors[i];
            }
        }

        private UserBook GetInstanceUserBook(bool isRead, bool isWish)
        {
            return new UserBook()
            {
                IdUser = User.Id,
                IdBook = Book.Id,
                IsRead = isRead,
                IsWish = isWish
            };
        }

        protected override Task FillObservableCollectionAsync()
        {
            throw new NotImplementedException();
        }
    }
}
