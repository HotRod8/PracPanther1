using PP.MAUIApp.ViewModels;
using PP_Library.Services;

namespace PP.MAUIApp.Views
{
    public partial class AddEmployee : ContentPage
    {
        public AddEmployee()
        {
            InitializeComponent();
        }

        private void AddClicked(object sender, EventArgs e)
        {
            (BindingContext as EmployeeViewModel).Add();
            Shell.Current.GoToAsync("//EmployeePage");
        }

        private void OnArriving(object sender, NavigatedToEventArgs e)
        {
            BindingContext = new EmployeeViewModel();
        }

        private void GoBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//EmployeePage");
        }
    }
}

