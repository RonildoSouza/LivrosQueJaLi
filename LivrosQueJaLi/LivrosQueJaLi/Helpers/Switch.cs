using System.Windows.Input;
using Xamarin.Forms;

namespace LivrosQueJaLi.Helpers
{
    public class Switch : Xamarin.Forms.Switch
    {
        public static readonly BindableProperty ToggledCommandProperty =
                BindableProperty.Create(nameof(ToggledCommand), typeof(ICommand), typeof(Switch), null);

        public ICommand ToggledCommand
        {
            get { return (ICommand)GetValue(ToggledCommandProperty); }
            set
            {
                SetValue(ToggledCommandProperty, value);
            }
        }

        public Switch() : base()
        {
            Initialize();
        }

        private void Initialize()
        {
            Toggled += Switch_Toggled;
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            if (ToggledCommand != null && ToggledCommand.CanExecute(null))
                ToggledCommand.Execute(e.Value);
        }
    }
}
