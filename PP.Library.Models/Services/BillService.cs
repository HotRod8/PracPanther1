using PP_Library.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_Library.Services
{
    public class BillService
    {
        private static BillService? instance;
        private static object daLock = new object();
        public static BillService Current
        {
            get
            {
                //For thread safety
                lock (daLock)
                {
                    //Critical Part - OS issue if used w/o lock
                    if (instance == null)
                    {
                        instance = new BillService();
                    }
                }

                return instance;
            }
        }

        private List<Bill> workBill;
        private BillService()
        {
            workBill = new List<Bill>
            {
                new Bill {DueDate = DateTime.MaxValue, TotalAmount = 12, TimeList = new List<Time>()},
                new Bill {DueDate = DateTime.Today, TotalAmount = 8, TimeList = new List<Time>()},
                new Bill {DueDate = DateTime.Now, TotalAmount = 2, TimeList = new List<Time>()}
            };
        }
        public List<Bill> Bills { get { return workBill; } }
        public void MakeBill(IEnumerable<Time> aList)
        {
            decimal sum = 0;
            foreach (var a in aList) 
            { 
                Employee employee = EmployeeService.Current.Get(a.EmployeeId);
                sum += a.Hours * employee.Id;
            }
            Bill newBill = new Bill { TotalAmount = sum, TimeList = aList };
            workBill.Add(newBill);
        }
        public void MakeAllBills(IEnumerable<Time> fullList)
        { 
        
        }
    }
}
