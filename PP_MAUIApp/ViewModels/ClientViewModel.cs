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
    //Model for the Client View
    public class ClientViewModel
    {
        //Still see Model's data when Add and Edit have been pulled up for the first time.
        public Client Model {  get; set;  }

        public string Display
        {
            get
            {
                return Model.ToString() ?? string.Empty;
            }
        }

        public ICommand DeleteCommand { get; private set; }
        public ICommand EditCommand { get; private set; }
        public ICommand ViewProjsCommand { get; private set; }

        public void ExecuteDelete(int id)
        {
            ProjLinker.Current.Delete(id);
        }
        public void ExecuteEdit(int id)
        {
            //Can use this approach or a different approach
            Shell.Current.GoToAsync($"//EditClient?clientId={id}");
        }
        public void StartProjects(int Id)
        {
            Shell.Current.GoToAsync($"//ClientProjects?clientId={Id}");
        }

        private void SetupCommands()
        {
            DeleteCommand = new Command(
                (c) => ExecuteDelete((c as ClientViewModel).Model.Id));
            EditCommand = new Command(
                (c) => ExecuteEdit((c as ClientViewModel).Model.Id));
            ViewProjsCommand = new Command(
                (c) => StartProjects((c as ClientViewModel).Model.Id));
        }

        //What are these 3 constructors for?
        public ClientViewModel(Client client)
        {
            Model = client;
            SetupCommands();
        }

        public ClientViewModel(int clientId)
        {
            Model = ProjLinker.Current.Get(clientId);
            SetupCommands();
        }

        public ClientViewModel()
        {
            Model = new Client();
            SetupCommands();
        }

        public void Add()
        {
            ProjLinker.Current.Add(Model);
            Model = new Client();
        }
        public void Update() 
        {
            ProjLinker.Current.Edit(Model);
            Model = new Client();
        }
        public void CloseClient()
        {
            ProjLinker.Current.CloseClient(Model);
        }

    }
}
