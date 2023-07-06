using PP.MAUIApp.ViewModels;
using PP_Library.Models;

namespace PP.MAUIApp.Views
{
    [QueryProperty(nameof(ClientId), "clientId")]
    public partial class AddProjectView : ContentPage
    {
        public int ClientId { get; set; }
        public AddProjectView()
        {
            InitializeComponent();
            //Problem lies below. ClientId isn't being set in time. 
            
        }

        private void GoBackClicked(object sender, EventArgs e)
        {
            var clientId = (BindingContext as ProjectViewModel).Client_Id;
            Shell.Current.GoToAsync($"//ClientProjects?clientId={clientId}");
        }

        private void AddClicked(object sender, EventArgs e)
        {
            (BindingContext as ProjectViewModel).Add();
            var clientId = (BindingContext as ProjectViewModel).Client_Id;
            Shell.Current.GoToAsync($"//ClientProjects?clientId={clientId}");
        }

        private void OnArriving(object sender, NavigatedToEventArgs e)
        {
            BindingContext = new ProjectViewModel { Client_Id = ClientId };
        }
    }
}

