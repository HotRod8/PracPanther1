using PP.MAUIApp.ViewModels;
using PP_Library.Models;
using PP_Library.Services;

namespace PP.MAUIApp.Views 
{
    [QueryProperty(nameof(ClientId), "clientId")]
    public partial class ClientProjsView : ContentPage
    {
        public int ClientId { get; set; }
        public ClientProjsView()
        {
            InitializeComponent();
        }

        private void DeleteAllClicked(object sender, EventArgs e)
        {
            ProjService.Current.DeleteAll(ClientId);
            Shell.Current.GoToAsync("//ClientPage");
        }

        private void GoBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ClientPage");
        }
        
        private void AddClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//AddProject?clientId={ClientId}");
        }

        private void EditClicked(object sender, EventArgs e)
        {
            (BindingContext as ProjectViewViewModel).RefreshProjectList();
        }

        private void ClickedSearch(object sender, EventArgs e)
        {
            (BindingContext as ProjectViewViewModel).Search();
        }

        private void OnArrived(object sender, EventArgs e) 
        {
            BindingContext = new ProjectViewViewModel(ClientId);
            (BindingContext as ProjectViewViewModel).RefreshProjectList(); 
        }

        private void CloseClicked(object sender, EventArgs e)
        {
            (BindingContext as ProjectViewViewModel).RefreshProjectList();
        }

        private void CloseAllClicked(object sender, EventArgs e)
        {
            ProjService.Current.CloseAll(ClientId);
            Shell.Current.GoToAsync("//ClientPage");
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as ProjectViewViewModel).RefreshProjectList();
        }

        private void BillsClicked(object sender, EventArgs e)
        {
            (BindingContext as ProjectViewViewModel).RefreshProjectList();
        }

        private void AllBillsClicked(object sender, EventArgs e)
        {
            var ProjTimes = TimeService.Current.Times;
            BillService.Current.MakeAllBills(ProjTimes);
            (BindingContext as ProjectViewViewModel).RefreshProjectList();
        }
    }
}

