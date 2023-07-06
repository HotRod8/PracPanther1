using PP.MAUIApp.ViewModels;
using PP_Library.Models;
using PP_Library.Services;

namespace PP.MAUIApp.Views 
{
    [QueryProperty ("EmplID","EmployeeID")]
    [QueryProperty ("ProjID","ProjectID")]
    public partial class TimePage : ContentPage
    {
        public int EmplID { get; set; }
        public int ProjID { get; set; }
        public TimePage()
        {
            InitializeComponent();
        }

        private void OnArrived(object sender, NavigatedToEventArgs e)
        {
            BindingContext = new TimePageViewModel(EmplID, ProjID);
            (BindingContext as TimePageViewModel).RefreshTimeList();
        }

        private void AddClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync($"//TimeDetailPage?emplId={0}&projId={0}");
        }

        private void GoBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }

        //Delete is mysteriously not refreshing.
        private void DeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as TimePageViewModel).RefreshTimeList();
        }

        private void EditClicked(object sender, EventArgs e)
        {
            //Edit Command takes care of transfer to Time Details Page
        }

        //Search results are mysteriously not refreshing.
        private void ClickedSearch(object sender, EventArgs e)
        {
            (BindingContext as TimePageViewModel).Search();
        }
    }
}

