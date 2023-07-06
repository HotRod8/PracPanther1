using PP.MAUIApp.ViewModels;
using PP_Library.Services;

namespace PP.MAUIApp.Views
{
    [QueryProperty("EmplID", "EmployeeId")]
    public partial class EditEmployee : ContentPage
    {
        public int EmplID { get; set; }
        public EditEmployee()
        {
            InitializeComponent();
        }

        private void OnArriving(object sender, NavigatedToEventArgs e)
        {
            BindingContext = new EmployeeViewModel(EmplID);
        }

        private void EditClicked(object sender, EventArgs e)
        {
            (BindingContext as EmployeeViewModel).Update();
            Shell.Current.GoToAsync("//EmployeePage");
        }

        private void GoBackClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//EmployeePage");
        }
    }
}

