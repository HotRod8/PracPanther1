using PP.MAUIApp.ViewModels;
using PP_Library.Models;
using PP_Library.Services;

namespace PP.MAUIApp.Views 
{

    public partial class TimePage : ContentPage
    {
        public TimePage()
        {
            InitializeComponent();
        }

        private void OnArrived(object sender, NavigatedToEventArgs e)
        {
            BindingContext = new TimePageViewModel();
            (BindingContext as TimePageViewModel).RefreshTimeList();
        }

        private void AddClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//TimeDetailPage");
        }

        private void GoBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as TimePageViewModel).RefreshTimeList();
        }

        private void EditClicked(object sender, EventArgs e)
        {
            //Edit Command takes care of transfer to Time Details Page
            Shell.Current.GoToAsync("//TimeDetailPage");
        }

        //Search results are mysteriously not refreshing.
        private void ClickedSearch(object sender, EventArgs e)
        {
            (BindingContext as TimePageViewModel).Search();
        }
    }
}

