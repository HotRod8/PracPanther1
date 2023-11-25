using PP_Library.Models;
using PP_Library.DTO;
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
        public ClientDTO Model {  get; set;  }
        public DateTime MinimumCloseDate => DateTime.Today;
        public DateTime MaximumCloseDate => DateTime.Today.AddDays(365);

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

        public void ExecuteDelete(ClientDTO model)
        {
            ProjLinker.Current.Delete(model);
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
                (c) => ExecuteDelete((c as ClientViewModel).Model));
            EditCommand = new Command(
                (c) => ExecuteEdit((c as ClientViewModel).Model.Id));
            ViewProjsCommand = new Command(
                (c) => StartProjects((c as ClientViewModel).Model.Id));
        }

        //What are these 3 constructors for?
        public ClientViewModel(ClientDTO client)
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
            Model = new ClientDTO();
            SetupCommands();
        }

        public void Add()
        {
            ProjLinker.Current.AddOrUpdate(Model);
        }
        public void Update() 
        {
            ProjLinker.Current.AddOrUpdate(Model);
        }
        public void CloseClient()
        {
            ProjLinker.Current.CloseClient(Model);
        }

    }
}
