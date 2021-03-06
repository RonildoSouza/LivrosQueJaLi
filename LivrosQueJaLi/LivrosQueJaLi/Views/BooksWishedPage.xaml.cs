﻿using LivrosQueJaLi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LivrosQueJaLi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BooksWishedPage : ContentPage
    {
        public BooksWishedPage()
        {
            InitializeComponent();
            BindingContext = new BooksWishedViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as BooksWishedViewModel)?.RefreshCommand.Execute(null);
        }
    }
}