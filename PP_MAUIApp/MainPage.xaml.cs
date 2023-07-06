using PP.MAUIApp.ViewModels;

namespace PP.MAUIApp
{
    public partial class MainPage : ContentPage
    {
        //    int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        //Direct the user to the same route in AppShell for a Client
        //Do this same thing for Employee and Time
        private void ClientsClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ClientPage");
        }

        private void EmployeesClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//EmployeePage");
        }

        private void TimeClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//TimePage");
        }
    }
}