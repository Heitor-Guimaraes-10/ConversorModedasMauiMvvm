using ConversorModedasMauiMvvm.ViewModels;

namespace ConversorModedasMauiMvvm
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new MainViewModel();
        }
    }
}