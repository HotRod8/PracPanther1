using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PP_Library.Models
{
    /*
         * An Int property called ProjectId
         * An Int property called EmployeeId
         * A DateTime property called Date
         * A String property called Narrative
         * An Int property called Hours
         */
    public class Time
    {
        //Consider making a TimeID to not unintentionally
        //delete multiple Time entries at once.
        public Time() 
        {
            ClientId = 0;
            ProjectId = 0;
            EmployeeId = 0;
            Date = DateTime.Now;
            Narrative = "";
            Hours = 0;
        }
        //Because ProjectId is only unique per client,
        //we must take in the ClientId to connect them
        public int ClientId { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public string? Narrative { get; set; }
        public int Hours { get; set; }

        public override string ToString()
        {
            return $"EmplID:[{EmployeeId}] ProjID:({ProjectId}) " +
                $"{Date}. {Hours} total hrs.\n{Narrative}";
        }
    }
    /* Allow users to supply CRUD (Create, Read, Update, and Delete) functionality to a Time entry on a Project.
     * 
     * Create a Singleton file connected to Project class.
     */
}
