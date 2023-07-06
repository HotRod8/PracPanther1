using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PP_Library.Models;
using PP_Library.Services;

namespace PP.MAUIApp.ViewModels
{
    //ViewModel for the Client View
    public class ClientViewViewModel : INotifyPropertyChanged
    {
        //We use an ObservableCollection to prevent some particular glitches that occur in 
        //here if we were to use List instead.
        public ObservableCollection<ClientViewModel> Clients
        {
            get
            {
                return new ObservableCollection<ClientViewModel>
                       (ProjLinker.Current.Search(Query).Select(c => new ClientViewModel(c)).ToList());
            }
        }

        public string Query { get; set; }

        public Client SelectedClient { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //This updates the "Clients" list through the Event Handler when called upon.
        public void Search()
        {
            NotifyPropertyChanged("Clients");
        }

        //Find a way to add a new Id, Name, and [Notes]. 
        public void Add(Client client)
        {
            ProjLinker.Current.Add(client);
            NotifyPropertyChanged("Clients");
        }

        public void Delete()
        {
            if (SelectedClient == null) {  return;  }
            ProjLinker.Current.Delete(SelectedClient.Id);
            SelectedClient=null;
            NotifyPropertyChanged("Clients");
            NotifyPropertyChanged("SelectedClient");
        }

        public void RefreshClientList()
        {
            //same as NotifyPropertyChanged("Clients");
            NotifyPropertyChanged(nameof(Clients));
        }
    }
}
