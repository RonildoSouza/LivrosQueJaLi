using LivrosQueJaLi.Models;
using LivrosQueJaLi.Models.Entities;
using LivrosQueJaLi.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LivrosQueJaLi.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookDetailPage : ContentPage
    {
        public BookDetailPage(Book pBook, UserBook pUserBook)
        {
            InitializeComponent();
            BindingContext = new BookDetailViewModel(pBook, pUserBook);
        }

        private void SwitchLent_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwitchBorrowed.IsToggled)
                SwitchBorrowed.IsToggled = !e.Value;

            if (SwitchSeeling.IsToggled)
                SwitchSeeling.IsToggled = !e.Value;

            if (SwitchSold.IsToggled)
                SwitchSold.IsToggled = !e.Value;
        }

        private void SwitchSeeling_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwitchSold.IsToggled)
                SwitchSold.IsToggled = !e.Value;

            if (SwitchLent.IsToggled)
                SwitchLent.IsToggled = !e.Value;

            if (SwitchBorrowed.IsToggled)
                SwitchBorrowed.IsToggled = !e.Value;
        }

        private void SwitchBorrowed_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwitchLent.IsToggled)
                SwitchLent.IsToggled = !e.Value;

            if (SwitchSeeling.IsToggled)
                SwitchSeeling.IsToggled = !e.Value;

            if (SwitchSold.IsToggled)
                SwitchSold.IsToggled = !e.Value;
        }

        private void SwitchSold_Toggled(object sender, ToggledEventArgs e)
        {
            if (SwitchSeeling.IsToggled)
                SwitchSeeling.IsToggled = !e.Value;

            if (SwitchLent.IsToggled)
                SwitchLent.IsToggled = !e.Value;

            if (SwitchBorrowed.IsToggled)
                SwitchBorrowed.IsToggled = !e.Value;
        }
    }
}