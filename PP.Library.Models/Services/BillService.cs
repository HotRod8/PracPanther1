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
            workBill = new List<Bill>();
        }
        public List<Bill> Bills { get { return workBill; } }

        public List<Bill> Search(string query)
        {
            //Doesn't like a null query
            return Bills.Where(s => s.ToString().ToUpper().Contains(query.ToUpper())).ToList();
        }
        public void MakeBill(IEnumerable<Time> aList)
        {
            decimal sum = 0;
            DateTime dateTime = DateTime.MinValue;
            foreach (var a in aList)
            { 
                Employee employee = EmployeeService.Current.Get(a.EmployeeId);
                //Inverse the hours logic here
                if (employee != null) { sum += a.Hours * employee.Rate; }
                Project project = ProjService.Current.Get(a.ProjectId);
                if (project != null && project.ClosedDate > dateTime) 
                { dateTime = project.ClosedDate; }
            }
            Bill newBill = new Bill { TotalAmount = sum, DueDate = dateTime, TimeList = aList };
            workBill.Add(newBill);
        }
        //Work on getting a bill for each project under one client
        public void MakeAllBills(IEnumerable<Time> fullList)
        {
            foreach (var member in fullList)
            {
                var templist = fullList.Where(l => l.ProjectId == member.ProjectId);
                MakeBill(templist);
            }
        }
    }
}
