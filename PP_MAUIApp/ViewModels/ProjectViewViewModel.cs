using PP_Library.Models;
using PP_Library.DTO;
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
        public ClientDTO Client { get; set; }
        public bool ShowOrHide { get; set; }
        public ObservableCollection<ProjectViewModel> Projects
        {
            get
            {
                return new ObservableCollection<ProjectViewModel>
                       (ProjService.Current.SearchByID(Client.Id, Query).Select(c => new ProjectViewModel(c)).ToList());
            }
        }
        public ObservableCollection<BillViewModel> Bills
        {
            get
            {
                if(Query == null)
                {
                    return new ObservableCollection<BillViewModel>
                       (BillService.Current.Search(string.Empty)
                       .Where(b => b.TimeList.Any(bl => bl.ClientId == Client.Id))
                       .Select(c => new BillViewModel(c)).ToList());
                }
                return new ObservableCollection<BillViewModel>
                       (BillService.Current.Search(Query)
                       .Where(b => b.TimeList.Any(bl => bl.ClientId == Client.Id))
                       .Select(c => new BillViewModel(c)).ToList());
            }
        }

        //We don't want the display to show the bills of other clients. How do we fix this?
        public ProjectViewViewModel(int clientId)
        {
            if (clientId > 0)
            {
                Client = ProjLinker.Current.Get(clientId);
            }
            else
            {
                Client = new ClientDTO();
            }
            if (Bills.Count > 0) { ShowOrHide = true; }
            else { ShowOrHide = false; }
        }

        private string quer { get; set; }
        public string Query {
            get { return quer; }
            set { quer = value ?? string.Empty; }
        }

        public Project SelectedProject { get; set; }
        public Bill SelectedBill { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //This updates the "Projects" list through the Event Handler when called upon.
        public void Search()
        {
            NotifyPropertyChanged("Projects");
            NotifyPropertyChanged("Bills");
        }

        public void AllBills()
        {
            BillService.Current.MakeAllBills(TimeService.Current.Times.Where(c => c.ClientId == Client.Id));
        }
        public void Open()
        {
            var list = BillService.Current.Bills;
            foreach (var item in list)
            { item.Unpaid = true; }
        }
        public void Close()
        {
            var list = BillService.Current.Bills;
            foreach (var item in list)
            { item.Unpaid = false; }
        }

        public void RefreshProjectList()
        {
            //same as NotifyPropertyChanged("Projects");
            NotifyPropertyChanged(nameof(Projects));
            NotifyPropertyChanged("Bills");
            NotifyPropertyChanged("ShowOrHide");
        }

    }
}
