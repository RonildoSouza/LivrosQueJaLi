using LivrosQueJaLi.Helpers;
using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Views;
using MvvmHelpers;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableRangeCollection<Book> Books { get; private set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public User User { get { return Constants.User; } }

        public Command BookDetailCommand { get; private set; }

        public BaseViewModel()
        {
            Books = new ObservableRangeCollection<Book>();
            BookDetailCommand = new Command(ExecuteBookDetailCommand);
        }

        protected virtual void ExecuteBookDetailCommand(object obj)
        {
            var book = obj as Book;
            NavigationToPush(new BookDetailTabbedPage(book));
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
    }
}
