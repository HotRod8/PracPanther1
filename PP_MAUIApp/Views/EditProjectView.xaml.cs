using PP.MAUIApp.ViewModels;
using PP_Library.Models;
using PP_Library.Services;

namespace PP.MAUIApp.Views 
{
    [QueryProperty(nameof(ClientId), "clientId")]
    [QueryProperty(nameof(ProjId), "projId")]
    public partial class EditProjectView : ContentPage
    {
        public int ClientId { get; set; }
        public int ProjId { get; set; }
        public EditProjectView()
        {
            InitializeComponent();
        }

        private void UpdateClicked(object sender, EventArgs e)
        {
            (BindingContext as ProjectViewModel).Update();
            var clientId = (BindingContext as ProjectViewModel).Client_Id;
            Shell.Current.GoToAsync($"//ClientProjects?clientId={clientId}");
        }

        private void GoBackClicked(object sender, EventArgs e)
        {
            var clientId = (BindingContext as ProjectViewModel).Client_Id;
            Shell.Current.GoToAsync($"//ClientProjects?clientId={clientId}");
        }

        //Need to find a way to get the selected project's ID
        private void OnArriving(object sender, NavigatedToEventArgs e)
        {
            BindingContext = new ProjectViewModel (ClientId, ProjId);
        }

        private void BillsClicked(object sender, EventArgs e)
        {

        }
    }
}

