using LivrosQueJaLi.Models;
using LivrosQueJaLi.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LivrosQueJaLi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetailTabbedPage : TabbedPage
    {
        public BookDetailTabbedPage(Book pBook, bool pIsVisible = false)
        {
            InitializeComponent();
            BindingContext = new BookDetailTabbedViewModel(pBook);

            Children.Add(new BookDetailPage(pBook, pIsVisible));
            Children.Add(new BookCommentsPage(pBook));
            Children.Add(new WhoReadPage(pBook));
        }
    }
}