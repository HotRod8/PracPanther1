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
        public int ProjectId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public string Narrative { 
            get { return Narrative; } 
            set { Narrative = value; } 
        }
        public int Hours { get; set; }

        public override string ToString()
        {
            return $"[{EmployeeId}] {ProjectId}. {Date}. {Hours}. \n{Narrative}";
        }
    }
    /* Allow users to supply CRUD (Create, Read, Update, and Delete) functionality to a Time entry on a Project.
     * 
     * Create a Singleton file connected to Project class.
     */
}
