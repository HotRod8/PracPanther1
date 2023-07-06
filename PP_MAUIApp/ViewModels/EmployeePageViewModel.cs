using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using PP_Library.Models;
using PP_Library.Services;

namespace PP.MAUIApp.ViewModels
{
    public class EmployeePageViewModel : INotifyPropertyChanged
    {
        public Employee NewHire { get; set; }
        //We use an ObservableCollection to prevent some particular glitches that occur in 
        //here if we were to use List instead.
        public ObservableCollection<EmployeeViewModel> Employees
        {
            get
            {
                return new ObservableCollection<EmployeeViewModel>
                        (EmployeeService.Current.Search(Query).Select(c => new EmployeeViewModel(c)).ToList());
            }
        }
        public EmployeePageViewModel()
        {
            foreach (var employee in Employees) 
            { 
                if(employee.Model.Id > 0) 
                { 
                    NewHire = employee.Model;
                }
                else NewHire = new Employee();
            }
        }

        public string Query { get; set; }

        public Employee SelectedEmployee { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //This updates the "Employees" list through the Event Handler when called upon.
        public void Search()
        {
            NotifyPropertyChanged("Employees");
        }

        //Find a way to add a new Id, Name, and [Notes]. 
        public void Add(Employee Employee)
        {
            EmployeeService.Current.Add(Employee);
            NotifyPropertyChanged("Employees");
        }

        public void Delete()
        {
            if (SelectedEmployee == null) { return; }
            EmployeeService.Current.Delete(SelectedEmployee.Id);
            SelectedEmployee = null;
            NotifyPropertyChanged("Employees");
            NotifyPropertyChanged("SelectedEmployee");
        }

        public void RefreshEmployeeList()
        {
            //same as NotifyPropertyChanged("Employees");
            NotifyPropertyChanged(nameof(Employees));
        }
    }
}
