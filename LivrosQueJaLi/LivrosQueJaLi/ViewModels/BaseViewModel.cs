using LivrosQueJaLi.DAL;
using LivrosQueJaLi.Helpers;
using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Views;
using MvvmHelpers;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private UserBookDAL _userBookDAL;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableRangeCollection<Book> Books { get; private set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private bool _isVisible = true;
        public bool IsVisible
        {
            get { return _isVisible; }
            set { SetProperty(ref _isVisible, value); }
        }

        public User User { get { return Constants.User; } }

        public Command BookDetailCommand { get; private set; }
        public Command PrivacyPolicyCommand { get; set; }

        public BaseViewModel()
        {
            _userBookDAL = new UserBookDAL();
            Books = new ObservableRangeCollection<Book>();
            BookDetailCommand = new Command(ExecuteBookDetailCommand);
            PrivacyPolicyCommand = new Command(ExecutePrivacyPolicyCommand);
        }

        private void ExecutePrivacyPolicyCommand() =>
            NavigationToPush(new PrivacyPolicyWebPage());

        protected virtual async void ExecuteBookDetailCommand(object obj)
        {
            var book = obj as Book;

            if (obj != null)
            {
                var userBook = await _userBookDAL.SelectUserBookByIds(User.Id, book.Id);
                NavigationToPush(new BookDetailTabbedPage(book, userBook));
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);

            return true;
        }

        protected async void FillListView(Func<Task> action)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    IsBusy = true;
                    await action();
                }
                else
                    DisplayAlertShow("Sem Acesso a Internet", "Falha de conexão com a internet!");
            }
            finally
            {
                IsBusy = false;
            }
        }

        protected abstract Task FillObservableCollectionAsync();

        protected async void NavigationToPush(Page pPage) => await App.Current.MainPage.Navigation.PushAsync(pPage);

        protected async void DisplayAlertShow(string title, string message) =>
            await App.Current.MainPage
                .DisplayAlert(title, message, "OK");

        protected void RemovePageFromStack<T>(INavigation pNavigation)
        {
            var existingPages = pNavigation.NavigationStack.ToList();
            foreach (var page in existingPages)
            {
                if (page.GetType() == typeof(T))
                    pNavigation.RemovePage(page);
            }
        }

        protected bool ValidEmail(string pEmail)
        {
            var regex = new Regex(@"[\w\.-]+(\+[\w-]*)?@([\w-]+\.)+[\w-]+");
            return regex.IsMatch(pEmail);
        }

        protected bool ValidPassword(string pPassword, string pConfirmPassword)
        {
            if (!string.IsNullOrEmpty(pPassword) && !string.IsNullOrEmpty(pConfirmPassword))
                return pConfirmPassword.Equals(pPassword);

            return false;
        }
    }
}
