using PP.MAUIApp.ViewModels;

namespace PP.MAUIApp.Views
{
    public partial class EmployeePage : ContentPage
    {
        public EmployeePage()
        {
            InitializeComponent();
        }

        private void OnArrived(object sender, NavigatedToEventArgs e)
        {
            BindingContext = new EmployeePageViewModel();
            (BindingContext as EmployeePageViewModel).RefreshEmployeeList();
        }

        private void GoBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//MainPage");
        }

        private void AddClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//AddEmployee");
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            (BindingContext as EmployeePageViewModel).RefreshEmployeeList();
        }

        private void EditClicked(object sender, EventArgs e)
        {
            //Shell.Current.GoToAsync("//EditEmployee");
        }

        private void ClickedSearch(object sender, EventArgs e)
        {
            (BindingContext as EmployeePageViewModel).Search();
        }
    }
}

