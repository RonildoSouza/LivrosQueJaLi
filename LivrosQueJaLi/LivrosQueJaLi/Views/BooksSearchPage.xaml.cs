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
    public partial class BooksSearchPage : ContentPage
    {
        public BooksSearchPage()
        {
            InitializeComponent();
            BindingContext = new BooksSearchViewModel();
        }
    }
}