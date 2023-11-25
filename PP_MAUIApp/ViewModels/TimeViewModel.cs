using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.ApplicationModel.Communication;
using PP_Library.Models;
using PP_Library.Services;

namespace PP.MAUIApp.ViewModels
{
    public class TimeViewModel : INotifyPropertyChanged
    {
        public bool Visible { get; set; }
        public ObservableCollection<Time> StopWatches
        {
            get
            {
                if (SelectedEmployee != null && SelectedProject != null)
                {
                    Visible = true;
                    return new ObservableCollection<Time>(TimeService.Current.Times.Where
                    (s => s.EmployeeId == SelectedEmployee.Id && s.ProjectId == SelectedProject.Id));
                }
                else
                {
                    Visible = false;
                    return null;
                }
            }
        }
        private Time TimeId;
        public Time IdToEdit 
        { 
            get
            {
                return TimeId;
            }
            set
            {
                TimeId = value ?? null;
                if (TimeId != null)
                {
                    Clock.Id = TimeId.Id;
                }

            }
        }
        public ObservableCollection<Employee> Employees
        {
            get
            {
                return new ObservableCollection<Employee>(EmployeeService.Current.Employees);
            }
        }
        private Employee selectedEmployee;
        public Employee SelectedEmployee { 
            get { 
                return selectedEmployee;
            } 
            set
            {
                selectedEmployee = value ?? null;
                if(selectedEmployee != null)
                {
                    Clock.EmployeeId = selectedEmployee.Id;
                }
            }
        }
        public ObservableCollection<Project> Projects
        {
            get
            {
                return new ObservableCollection<Project>(ProjService.Current.Projects);
            }
        }
        private Project selectedProject;
        public Project SelectedProject {
            get
            {
                return selectedProject;
            }
            set
            {
                selectedProject = value ?? null;
                if (selectedProject != null)
                {
                    Clock.ProjectId = selectedProject.Id;
                    Clock.ClientId = selectedProject.ClientId;
                }
            }
        }
        private Time watch;
        public Time Clock {
            get
            { 
                return watch;
            }
            set
            { 
                watch = value ?? new Time();
            }
        }

        public string Display
        {
            get
            {
                return Clock.ToString() ?? string.Empty;
            }
        }

        public ICommand DeleteCommand { get; private set; }
        public ICommand TimerCommand { get; private set; }

        private void ExecuteDelete(int empId, int projid, int timeid)
        {
            Time temp = TimeService.Current.Get(empId, projid, timeid);
            TimeService.Current.Delete(temp);
        }
        /*  private void ExecuteTimer()
            {
                var window = new Window(new TimerView(Clock.Id))
                {
                    Width = 250,
                    Height = 350,
                    X = 0,
                    Y = 0
                };
                Application.Current.OpenWindow(window);
            }*/
        private void SetupCommands()
        {
            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as TimeViewModel).Clock.EmployeeId, (c as TimeViewModel).Clock.ProjectId, (c as TimeViewModel).Clock.Id));
            //    TimerCommand = new Command(ExecuteTimer);
        }

        //Constructors must finish before outside variables are set.
        public TimeViewModel(Time time)
        //Using Times from TimeViewViewModel to get individual Times,
        //which lets us get the properties of a single Time
        {
            Clock = time;
            SetupCommands();
        }
        //Need to find a way to get the selected Time's ID
        public TimeViewModel()
        {
            if (SelectedEmployee == null && SelectedProject == null && IdToEdit == null && Clock == null)
            {    Clock = new Time();    }
            else Clock = TimeService.Current.Get(SelectedEmployee.Id, SelectedProject.Id, IdToEdit.Id);
            SetupCommands();
        }

        public void AddorUpdate()
        {
            var test = TimeService.Current.Times.
                FirstOrDefault(t => t.ProjectId == SelectedProject.Id && t.EmployeeId == SelectedEmployee.Id);
            if (test == null)
            {    TimeService.Current.Add(Clock);    }
            else if(test != null && IdToEdit == null)
            {    TimeService.Current.Add(Clock);    }
            else TimeService.Current.Edit(Clock);
            
        }
        public void MakeVisible()
        {
            if(SelectedEmployee != null && SelectedProject != null) { Visible = true; }
        }
        public void MakeInvisible()
        {
            Visible = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RefreshTimeList()
        {
            //same as NotifyPropertyChanged("Projects");
            NotifyPropertyChanged(nameof(Projects));
            NotifyPropertyChanged(nameof(Employees));
            NotifyPropertyChanged(nameof(StopWatches));
            NotifyPropertyChanged(nameof(Visible));
        }

        public void RefreshVisibility()
        { 
            NotifyPropertyChanged(nameof(Visible));
            NotifyPropertyChanged(nameof(StopWatches));
        }
    }
}
