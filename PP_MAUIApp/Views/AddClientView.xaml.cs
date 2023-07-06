using PP.MAUIApp.ViewModels;

namespace PP.MAUIApp.Views
{
    public partial class AddClientView : ContentPage
    {
        public AddClientView()
        {
            InitializeComponent();
            BindingContext = new ClientViewModel();
        }

        private void AddClicked(object sender, EventArgs e)
        {
            (BindingContext as ClientViewModel).Add();
            Shell.Current.GoToAsync("//ClientPage");
        }

        private void GoBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ClientPage");
        }
    }
}

