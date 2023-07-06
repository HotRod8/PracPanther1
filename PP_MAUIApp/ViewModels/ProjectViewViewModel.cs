using PP_Library.Models;
using PP_Library.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PP.MAUIApp.ViewModels
{
    //ViewModel for the Project View
    public class ProjectViewViewModel : INotifyPropertyChanged
    {
        //We use an ObservableCollection to prevent some particular glitches that occur in 
        //here if we were to use List instead.
        public Client Client { get; set; }
        public ObservableCollection<ProjectViewModel> Projects
        {
            get
            {
                return new ObservableCollection<ProjectViewModel>
                       (ProjService.Current.SearchByID(Client.Id, Query).Select(c => new ProjectViewModel(c)).ToList());
            }
        }

        public ProjectViewViewModel(int clientId)
        {
            if (clientId > 0)
            {
                Client = ProjLinker.Current.Get(clientId);
            }
            else
            {
                Client = new Client();
            }

        }

        private string quer { get; set; }
        public string Query {
            get { return quer; }
            set { quer = value ?? string.Empty; }
        }

        public Project SelectedProject { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //This updates the "Projects" list through the Event Handler when called upon.
        public void Search()
        {
            NotifyPropertyChanged("Projects");
        }

        public void RefreshProjectList()
        {
            //same as NotifyPropertyChanged("Projects");
            NotifyPropertyChanged(nameof(Projects));
        }
    }
}
