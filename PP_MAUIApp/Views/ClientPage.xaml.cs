using PP.MAUIApp.ViewModels;

namespace PP.MAUIApp.Views
{
    public partial class ClientPage : ContentPage
    {
        public ClientPage()
        {
            InitializeComponent();
            //BindingContext = new ClientViewViewModel();
        }

        private void ClickedSearch(object sender, EventArgs e)
        {
            (BindingContext as ClientViewViewModel).Search();
        }

        //Find a way to add a new Id, Name, and [Notes] from user input
        // without using command line.
        private void AddClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//AddClient");
        }

        private void EditClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//EditClient");
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as ClientViewViewModel).RefreshClientList();
        }

        private void GoBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }

        private void OnArrived(object sender, NavigatedToEventArgs e)
        {
            BindingContext = new ClientViewViewModel();
            (BindingContext as ClientViewViewModel).RefreshClientList();
        }

        private void ProjsClicked(object sender, EventArgs e)
        {
            (BindingContext as ClientViewViewModel).RefreshClientList();
        }
    }
}