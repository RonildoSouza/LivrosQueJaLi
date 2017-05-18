using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.Views;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LivrosQueJaLi.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public User User { get; private set; }

        //private User _user;   
        //public User User
        //{
        //    get { return _user; }
        //    set { if (_user == null) _user = value; }
        //} 

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public Command BookDetailCommand { get; set; }

        public BaseViewModel()
        {
            BookDetailCommand = new Command(ExecuteBookDetailCommand);
        }

        public BaseViewModel(User pUser) : this()
        {
            User = pUser;
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

        protected async void NavigationToPush(Page pPage) => await App.Current.MainPage.Navigation.PushAsync(pPage);

        protected async void DisplayAlertShow(string title, string message) =>
            await App.Current.MainPage
                .DisplayAlert(title, message, "OK");
    }
}
