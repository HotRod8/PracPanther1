using PP_Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_Library.Models
{ 
    public class Client
    {
        public Client()
        {
            IsActive = true;
            Id = 0;
            Name = string.Empty;
            OpenDate = DateTime.Today;
        }
        public Client(ClientDTO dto)
        {
            this.Id = dto.Id;
            this.Name = dto.Name;
            this.Notes = dto.Notes;
            this.OpenDate = dto.OpenDate;
            this.Projects = dto.Projects;
            this.IsActive = dto.IsActive;
        }
        /*
         * An Int property called Id
         * A DateTime property called OpenDate
         * A DateTime property called ClosedDate
         * A Boolean property called IsActive
         * A String property called Name
         * A String property called Notes
         */
        public int Id { get; set; }
        public string Name { get; set; }

        // the ? make Notes nullable
        public string? Notes { get; set; }
        public bool IsActive { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public List<Project>? Projects { get; set; }

        public override string ToString()
        {
            if (IsActive == true)
            {
                if(Projects == null) { return $"{Id}. {Name}. {Notes}. #0"; }
                return $"{Id}. {Name}. {Notes}. #{Projects.Count}";
            }
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
