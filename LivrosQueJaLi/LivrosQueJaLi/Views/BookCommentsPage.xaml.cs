using LivrosQueJaLi.Models;
using LivrosQueJaLi.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LivrosQueJaLi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookCommentsPage : ContentPage
    {
        public BookCommentsPage(Book pBook)
        {
            InitializeComponent();
            BindingContext = new BookCommentsViewModel(pBook);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            (BindingContext as BookCommentsViewModel)?.RefreshCommand.Execute(null);
        }
    }
}