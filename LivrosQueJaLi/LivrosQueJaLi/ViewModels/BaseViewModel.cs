using LivrosQueJaLi.Models.Entities;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace LivrosQueJaLi.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public User User { get; set; }

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public BaseViewModel()
        {
            User = new User()
            {
                Id = "afb376f1-3198-49d8-83a5-b8a8e86ae741",
                IdFacebook = "abc123"
            };
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

        protected void FillListView(Action action)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                IsBusy = true;

                action();

                IsBusy = false;
            }
            else
                App.Current.MainPage
                    .DisplayAlert("Sem Acesso a Internet", "Falha de conexão com a internet!", "OK");

        }
    }
}
