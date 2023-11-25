using PP.MAUIApp.ViewModels;
using PP_Library.Models;
using PP_Library.Services;

namespace PP.MAUIApp.Views
{
    public partial class TimeDetailPage : ContentPage
    {
        public TimeDetailPage()
        {
            InitializeComponent();
        }

        private void SaveClicked(object sender, EventArgs e)
        {
            (BindingContext as TimeViewModel).AddorUpdate();
            Shell.Current.GoToAsync("//TimePage");
        }

        private void OnArrived(object sender, NavigatedToEventArgs e)
        {
            BindingContext = new TimeViewModel();
            (BindingContext as TimeViewModel).RefreshTimeList();
        }

        private void GoBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//TimePage");
        }

        private void OpenClicked(object sender, EventArgs e)
        {
            (BindingContext as TimeViewModel).MakeVisible();
            (BindingContext as TimeViewModel).RefreshVisibility();
        }
        private void CloseClicked(object sender, EventArgs e)
        {
            (BindingContext as TimeViewModel).MakeInvisible();
            (BindingContext as TimeViewModel).RefreshVisibility();
        }
    }
}

