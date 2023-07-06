using PP.MAUIApp.ViewModels;

namespace PP.MAUIApp.Views
{
    [QueryProperty(nameof(ClientId), "clientId")]
    public partial class EditClientView : ContentPage
    {
        //Take in ClientId from the EditCommand 
        public int ClientId { get; set; }
        public EditClientView()
        {
            InitializeComponent();
        }

        private void UpdateClicked(object sender, EventArgs e)
        {
            (BindingContext as ClientViewModel).Update();
            Shell.Current.GoToAsync("//ClientPage");
        }

        private void GoBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ClientPage");
        }

        private void OnArriving(object sender, NavigatedToEventArgs e)
        {
            BindingContext = new ClientViewModel(ClientId);
        }

        private void CloseClicked(object sender, EventArgs e)
        {
            (BindingContext as ClientViewModel).CloseClient();
        }
    }
}

