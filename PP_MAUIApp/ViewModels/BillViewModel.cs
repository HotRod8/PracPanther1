using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PP_Library.Models;
using PP_Library.Services;

namespace PP.MAUIApp.ViewModels
{
    public class BillViewModel
    {
        public Bill Tab { get; set; }
        public BillViewModel(Bill bill) 
        {
            Tab = bill;
        }

        public string DisplayBills
        {
            get
            {
                return Tab.ToString() ?? string.Empty;
            }
        }


    }
}
