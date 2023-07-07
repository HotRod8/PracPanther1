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
    [QueryProperty(nameof(Client_Id), "ClientId")]
    public class ProjectViewModel
    {
        private int client_Id;
        public int Client_Id {
            get
            {
                return client_Id;
            }
            set
            {
                if(value > 0) {
                    client_Id = value;
                }
            }
        }
        public int proj_Id { get; set; }
        public Project Blueprint { get; set; }

        public string Display
        {
            get
            {
                return Blueprint.ToString() ?? string.Empty;
            }
        }

        public ICommand CloseCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand NewBillCommand { get; private set; }
        public ICommand TimerCommand { get; private set; }
        

        private void ExecuteClose(int clientId, int id)
        {
            Project temp = ProjService.Current.GetProj(clientId, id);
            ProjService.Current.Close(temp);
        }
        private void ExecuteDelete(int clientId, int id)
        {
            Project temp = ProjService.Current.GetProj(clientId, id);
            ProjService.Current.Delete(temp);
        }
        private void ExecuteEdit(int id, int projId)
        {
            //Can use this approach or a different approach
            Shell.Current.GoToAsync($"//EditProject?clientId={id}&projId={projId}");
        }
        private void CreateNewBill(int clientId, int id)
        {
            Project temp = ProjService.Current.GetProj(clientId, id);
            IEnumerable<Time> list = TimeService.Current.Times;
            IEnumerable<Time> templist = list.Where(p => p.ProjectId == temp.Id && p.ClientId == temp.ClientId).ToList();
            BillService.Current.MakeBill(templist);
        }
        /*  private void ExecuteTimer()
            {
                var window = new Window(new TimerView(Model.Id))
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
            CloseCommand = new Command(
                (c) => ExecuteClose((c as ProjectViewModel).Blueprint.ClientId, (c as ProjectViewModel).Blueprint.Id));
            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as ProjectViewModel).Blueprint.ClientId, (c as ProjectViewModel).Blueprint.Id));
            EditCommand = new Command(
                (c) => ExecuteEdit((c as ProjectViewModel).Blueprint.ClientId, (c as ProjectViewModel).Blueprint.Id));
            NewBillCommand = new Command(
                (c) => CreateNewBill((c as ProjectViewModel).Blueprint.ClientId, (c as ProjectViewModel).Blueprint.Id));
            //    TimerCommand = new Command(ExecuteTimer);
        }

        //Constructors must finish before outside variables are set.
        public ProjectViewModel(Project Project) 
        //Using projects from ProjectViewViewModel to get individual projects,
        //which lets us get the properties of a single project
        {
            if(Project == null) 
            {
                Blueprint = new Project();
            }
            else Blueprint = Project;
            SetupCommands();
        }
        //Need to find a way to get the selected project's ID
        public ProjectViewModel(int clientId, int projId) 
        {
            Client_Id = clientId;
            proj_Id = projId;
            Blueprint = ProjService.Current.GetProj(Client_Id, proj_Id);
            SetupCommands();
        }
        public ProjectViewModel()
        {
            //ClientId = Client_Id;
            Blueprint = new Project();
            SetupCommands();
        }

        public void Add()
        {
            Blueprint.ClientId = Client_Id;
            ProjService.Current.Add(Blueprint);
        }
        public void Update()
        {
            Blueprint = ProjService.Current.GetProj(Client_Id, proj_Id);
            ProjService.Current.Edit(Blueprint);
        }
    }
}
