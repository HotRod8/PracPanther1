using PP.MAUIApp.ViewModels;
using PP_Library.Models;
using PP_Library.Services;

namespace PP.MAUIApp.Views
{
    [QueryProperty(nameof(Employ_Id), "emplId")]
    [QueryProperty(nameof(Proj_Id), "projId")]
    [QueryProperty(nameof(Client_Id), "clientId")]
    public partial class TimeDetailPage : ContentPage
    {
        public int Employ_Id { get; set; }
        public int Proj_Id { get; set; }
        public int Client_Id { get; set; }
        public TimeDetailPage()
        {
            InitializeComponent();
        }

        private void SaveClicked(object sender, EventArgs e)
        {
            (BindingContext as TimeViewModel).AddorUpdate(Employ_Id, Proj_Id, Client_Id);
            Employ_Id = (BindingContext as TimeViewModel).Employ_Id;
            Proj_Id = (BindingContext as TimeViewModel).Proj_Id;
            Client_Id = (BindingContext as TimeViewModel).Client_Id;
            Shell.Current.GoToAsync("//TimePage");
        }

        private void OnArrived(object sender, NavigatedToEventArgs e)
        {
            BindingContext = new TimeViewModel(Employ_Id, Proj_Id, Client_Id);
            (BindingContext as TimeViewModel).RefreshTimeList();
        }

        private void GoBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//TimePage");
        }
    }
}

