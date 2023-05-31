using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PP_Library.Models
{
    public class Project
    {
        /*
         * An Int property called Id
         * A DateTime property called OpenDate
         * A DateTime property called ClosedDate
         * A Boolean property called IsActive
         * A String property called ShortName
         * A String property called LongName
         * An Int property called ClientId
         */
 //       public Client? Maker { get; set; }
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ShortName { get; set; }
        public string LongName { get; set; }
        public bool IsActive { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime ClosedDate { get; set; }

        public override string ToString()
        {
            if (IsActive == true){ return $"{ClientId}. {Id}. {ShortName}. {LongName}"; }
            return "Account inactive or does not exist.";
        }
    }
    /*Allow users to supply CRUD (Create, Read, Update, and Delete) functionality for Clients to a list of Clients
     * Allow users to supply CRUD (Create, Read, Update, and Delete) functionality for Projects to a list of Projects
     * 
     * Allow users the ability to link a Project object to a Client object using the Project.ClientId property
     * (Aggregation usage here)
     */
}
