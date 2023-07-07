using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_Library.Models
{
    public class Bill
    {
        /*You must have a TotalAmount property that shows the amount of the bill as calculated 
         *by multiplying the rate of the employee on a time entry by the number of hours on 
         *that time entry and then summing all such time entries on the project(s).
         *
         *You must have a DueDate property that shows the date the bill is due.
         */
        public decimal TotalAmount { get; set; }
        public DateTime DueDate { get; set; }
        //To take in multiple time instances
        public IEnumerable<Time> TimeList { get; set; }
        //Project class is connected throught the Time class (Time.ProjectId)
        public Bill() 
        {
            TimeList = new List<Time>();
            TotalAmount = 0;
            DueDate = DateTime.Now;
        }
    }
}
