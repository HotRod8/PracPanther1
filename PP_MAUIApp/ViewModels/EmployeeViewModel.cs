using PP_Library.Models;
using PP_Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PP.MAUIApp.ViewModels
{
    //Model for the Employee View
    public class EmployeeViewModel
    {
        //Still see Model's data when Add and Edit have been pulled up for the first time.
        public Employee Model { get; set; }

        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }

        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }

        public void ExecuteDelete(int id)
        {
            EmployeeService.Current.Delete(id);
        }
        public void ExecuteEdit(int id)
        {
            //Can use this approach or a different approach
            Shell.Current.GoToAsync($"//EditEmployee?EmployeeId={id}");
        }

        private void SetupCommands()
        {
            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as EmployeeViewModel).Model.Id));
            EditCommand = new Command(
                (c) => ExecuteEdit((c as EmployeeViewModel).Model.Id));
        }

        //What are these 3 constructors for?
        public EmployeeViewModel(Employee Employee)
        {
            Model = Employee;
            SetupCommands();
        }

        public EmployeeViewModel(int EmployeeId)
        {
            Model = EmployeeService.Current.Get(EmployeeId);
            SetupCommands();
        }

        public EmployeeViewModel()
        {
            Model = new Employee();
            SetupCommands();
        }

        public void Add()
        {
            EmployeeService.Current.Add(Model);
            Model = new Employee();
        }
        public void Update()
        {
            EmployeeService.Current.Edit(Model);
            Model = new Employee();
        }
    }
}

