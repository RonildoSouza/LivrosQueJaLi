using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Views;
using System;
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

        private UserBook _userBook;
        public UserBook UserBook
        {
            get { return _userBook; }
            set { SetProperty(ref _userBook, value); }
        }

        private string _authors = string.Empty;
        public string Authors
        {
            get { return _authors; }
            set { SetProperty(ref _authors, value); }
        }

        private string _price = string.Empty;
        public string Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

        public Command ReadCommand { get; }
        public Command WishCommand { get; }
        public Command InterestedUsersCommand { get; }
        public Command SaveChangesCommand { get; }

        public BookDetailViewModel(Book pBook, UserBook pUserBook)
        {
            _book = pBook;
            _userBook = pUserBook;

            IsVisible = pUserBook?.IsRead ?? false;
            _price = $"{_book.SaleInfo.RetailPrice.CurrencyCode} {_book.SaleInfo.RetailPrice.Amount}";
            _userBookDAL = new UserBookDAL();

            ReadCommand = new Command(ExecuteReadCommand);
            WishCommand = new Command(ExecuteWishCommand);
            InterestedUsersCommand = new Command(ExecuteInterestedUsersCommand);

            SaveChangesCommand = new Command(ExecuteSaveChangesCommand);

            FillAuthors();
        }

        private void ExecuteSaveChangesCommand(object obj)
        {
            _userBookDAL.InsertOrUpdate(UserBook);
            DisplayAlertShow("Sucesso", "Alterações salvas com sucesso!");
        }

        private void ExecuteInterestedUsersCommand(object obj)
        {
            if (_book != null)
                NavigationToPush(new InterestedUsersPage(_book));
        }

        private async void ExecuteReadCommand(object obj)
        {
            try
            {
                _userBook = await _userBookDAL.SelectUserBookByIds(User.Id, _book.Id);

                if (_userBook == null)
                    _userBook = GetInstanceUserBook(true, false);
                else
                    SetUserBook(ref _userBook, true, false);

                _userBookDAL.InsertOrUpdate(_userBook);

                if (string.IsNullOrEmpty(_userBook.Id))
                    UserBook = await _userBookDAL.SelectUserBookByIds(User.Id, _book.Id);

                DisplayAlertShow("Lido", $"Livro [{_book.VolumeInfo.Title}] adicionado a lista de lidos!");
                IsVisible = true;
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
                _userBook = await _userBookDAL.SelectUserBookByIds(User.Id, _book.Id);

                if (_userBook == null)
                    _userBook = GetInstanceUserBook(false, true);
                else
                    SetUserBook(ref _userBook, false, true);

                _userBookDAL.InsertOrUpdate(_userBook);

                if (string.IsNullOrEmpty(_userBook.Id))
                    UserBook = await _userBookDAL.SelectUserBookByIds(User.Id, _book.Id);

                DisplayAlertShow("Desejado", $"Livro [{_book.VolumeInfo.Title}] adicionado a lista de desejados!");
                IsVisible = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void FillAuthors()
        {
            if (_book.VolumeInfo.Authors != null)
            {
                for (int i = 0; i < _book.VolumeInfo.Authors.Length; i++)
                    Authors +=
                        (_book.VolumeInfo.Authors.Length - i > 1)
                        ? $"{_book.VolumeInfo.Authors[i]}, " : _book.VolumeInfo.Authors[i];
            }
        }

        private UserBook GetInstanceUserBook(bool isRead, bool isWish)
        {
            return new UserBook()
            {
                IdUser = User.Id,
                IdBook = _book.Id,
                IsRead = isRead,
                IsWish = isWish,
                Lent = false,
                Borrowed = false,
                Seeling = false,
                Sold = false
            };
        }

        private static void SetUserBook(ref UserBook userBook,
            bool pIsRead, bool pIsWish, bool pLent = false, bool pBorrowd = false,
            bool pSeeling = false, bool pSold = false)
        {
            userBook.IsRead = pIsRead;
            userBook.IsWish = pIsWish;
            userBook.Lent = pLent;
            userBook.Borrowed = pBorrowd;
            userBook.Seeling = pSeeling;
            userBook.Sold = pSold;
        }

        protected override Task FillObservableCollectionAsync() => throw new NotImplementedException();
    }
}
