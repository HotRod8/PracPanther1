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
    public class TimePageViewModel : INotifyPropertyChanged
    {
        //We use an ObservableCollection to prevent some particular glitches that occur in 
        //here if we were to use List instead.
        public Time Record { get; set; }
        public ObservableCollection<TimeViewModel> TimeLogs
        {
            get
            {
                if (Query == null)
                {
                    return new ObservableCollection<TimeViewModel>
                       (TimeService.Current.Search(string.Empty).Select(c => new TimeViewModel(c)).ToList());
                }
                return new ObservableCollection<TimeViewModel>
                       (TimeService.Current.Search(Query).Select(c => new TimeViewModel(c)).ToList());
            }
        }

        public TimePageViewModel(int empId, int projId, int cliId)
        {
            if ((empId > 0)&&(projId > 0))
            {
                Record = TimeService.Current.Get(empId, projId, cliId);
            }
            else
            {
                Record = new Time();
            }

        }

        private string quer { get; set; }
        public string Query
        {
            get { return quer; }
            set { quer = value ?? string.Empty; }
        }
        public Time SelectedRecord { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        //Use INotifyPropertyChanged to get TimeLogs again.
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Search()
        {
            NotifyPropertyChanged("TimeLogs");
        }

        public void RefreshTimeList()
        {
            //same as NotifyPropertyChanged("TimeLogs");
            NotifyPropertyChanged(nameof(TimeLogs));
        }
    }
}
