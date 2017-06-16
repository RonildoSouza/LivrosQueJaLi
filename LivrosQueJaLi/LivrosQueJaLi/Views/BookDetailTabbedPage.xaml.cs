using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LivrosQueJaLi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetailTabbedPage : TabbedPage
    {
        public BookDetailTabbedPage(Book pBook, UserBook pUserBook)
        {
            InitializeComponent();
            BindingContext = new BookDetailTabbedViewModel(pBook);

            Children.Add(new BookDetailPage(pBook, pUserBook));
            Children.Add(new BookCommentsPage(pBook));
            Children.Add(new WhoReadPage(pBook));
        }
    }
}