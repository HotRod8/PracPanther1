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
    [QueryProperty(nameof(Employ_Id), "EmpID")]
    [QueryProperty(nameof(Proj_Id), "ProjID")]
    [QueryProperty(nameof(Client_Id), "ClientID")] 
    public class TimeViewModel
    {
        public int Client_Id { get; set; }
        private int employ_Id;
        public int Employ_Id
        {
            get
            {
                return employ_Id;
            }
            set
            {
                if (value > 0)
                {
                    employ_Id = value;
                }
            }
        }
        public int proj_Id { get; set; }
        public int Proj_Id
        {
            get
            {
                return proj_Id;
            }
            set
            {
                if (value > 0)
                {
                    proj_Id = value;
                }
            }
        }
        public Time Clock { get; set; }

        public ObservableCollection<ClientViewModel> Clients
        {
            get
            {
                //if there is no client, we have nothing to return yet
                if (Clock == null || Clock.ClientId == 0)
                {
                    return new ObservableCollection<ClientViewModel>();
                }
                return new ObservableCollection<ClientViewModel>(ProjLinker
                    .Current.Clients.Where(p => p.Id == Clock.ClientId)
                    .Select(r => new ClientViewModel(r)));
            }
        }
        public ObservableCollection<ProjectViewModel> Projects
        {
            get
            {
                //if this is a new client, we have no projects to return yet
                if (Clock == null || Clock.ProjectId == 0)
                {
                    return new ObservableCollection<ProjectViewModel>();
                }
                return new ObservableCollection<ProjectViewModel>(ProjService
                    .Current.Projects.Where(p => p.Id == Clock.ProjectId)
                    .Select(r => new ProjectViewModel(r)));
            }
        }

        public ObservableCollection<EmployeeViewModel> Employees
        {
            get
            {
                //if this is a new client, we have no projects to return yet
                if (Clock == null || Clock.EmployeeId == 0)
                {
                    return new ObservableCollection<EmployeeViewModel>();
                }
                return new ObservableCollection<EmployeeViewModel>
                    (EmployeeService.Current.Employees.Where
                    (p => p.Id == Clock.EmployeeId).Select
                    (r => new EmployeeViewModel(r)));
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
        public ICommand EditCommand { get; private set; }
        public ICommand TimerCommand { get; private set; }

        private void ExecuteDelete(int empId, int projid)
        {
            Time temp = TimeService.Current.Get(empId, projid, Client_Id);
            TimeService.Current.Delete(temp);
        }
        private void ExecuteEdit(int empid, int projId)
        {
            //Can use this approach or a different approach
            Shell.Current.GoToAsync($"//TimeDetailPage?emplId={empid}&projId={projId}");
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
                (c) => ExecuteDelete((c as TimeViewModel).Clock.EmployeeId, (c as TimeViewModel).Clock.ProjectId));
            EditCommand = new Command(
                (c) => ExecuteEdit((c as TimeViewModel).Clock.EmployeeId, (c as TimeViewModel).Clock.ProjectId));
            //    TimerCommand = new Command(ExecuteTimer);
        }

        //Constructors must finish before outside variables are set.
        public TimeViewModel(Time Time)
        //Using Times from TimeViewViewModel to get individual Times,
        //which lets us get the properties of a single Time
        {
            if (Time == null)
            {
                Clock = new Time();
            }
            else Clock = Time;
            SetupCommands();
        }
        //Need to find a way to get the selected Time's ID
        public TimeViewModel(int empId, int projId, int cliId)
        {
            Employ_Id = empId;
            Proj_Id = projId; 
            Client_Id = cliId;
            Clock = TimeService.Current.Get(Employ_Id, Proj_Id, Client_Id);
            if (Clock == null) { Clock = new Time(); }
            SetupCommands();
        }
        public TimeViewModel()
        {
            //ClientId = employ_Id;
            Clock = new Time();
            SetupCommands();
        }

        public void AddorUpdate(int eid, int pid, int cid)
        {
            var test1 = ProjService.Current.Projects.
                FirstOrDefault(p => p.Id == pid && p.ClientId == cid);
            var test2 = EmployeeService.Current.Employees.FirstOrDefault(e => e.Id == eid);
            var test3 = TimeService.Current.Times.
                FirstOrDefault(t => t.ProjectId == pid && t.ClientId == cid && t.EmployeeId == eid);
            if (test1 != null && test2 != null && test3 == null)
            {
                TimeService.Current.Add(Clock);
            }
            else if((test1 == null || test2 == null) && test3 == null)
            {
                return;
            }
            else
            {
                if (Clock == null) 
                { Clock = TimeService.Current.Get(eid, pid, cid); }
                TimeService.Current.Edit(Clock);
                Clock = new Time();
            }
            
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
        }
    }
}
