using PP_Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PP_Library.DTO
{
    public class ClientDTO
    {
        public ClientDTO()
        {
            Name = string.Empty;
            IsActive = true;
            OpenDate = DateTime.Now;
            Projects = new List<Project>();
        }
        public ClientDTO(Client c)
        {
            this.Id = c.Id;
            this.Name = c.Name;
            this.Notes = c.Notes;
            this.IsActive = c.IsActive;
            this.OpenDate = c.OpenDate;
            this.ClosedDate = c.ClosedDate;
            this.Projects = c.Projects ?? new List<Project>();

            //watch for this warning ^
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string? Notes { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public bool IsActive { get; set; }
        public List<Project> Projects { get; set; }
        public override string ToString()
        {
            return $"{Id} {Name}, {Notes}";
        }
    }
}
