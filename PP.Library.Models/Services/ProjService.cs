using PP_Library.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PP_Library.Services
{
    public class ProjService
    {
        private static ProjService? instance;
        private static object daLock = new object();
        public static ProjService Current
        {
            get
            {
                //For thread safety
                lock (daLock)
                {
                    //Critical Part - OS issue if used w/o lock
                    if (instance == null)
                    {
                        instance = new ProjService();
                    }
                }

                return instance;
            }
        }

        private List<Project> customers;
        private ProjService()
        {
            customers = new List<Project>();
        }
        public List<Project> Projects { get { return customers; } }

        //Modify it to also search for clientId
        public Project? Get(int Id)
        {
            return customers.FirstOrDefault(e => e.Id == Id);
        }
        public IEnumerable<Project?> GetProjs(int cliId)
        {
            return customers.Where(e => e.ClientId == cliId);
        }
        public Project? GetProj(int cliId, int prId)
        {
            return customers.FirstOrDefault(e => (e.Id == prId) && (e.ClientId == cliId));
        }

        public IEnumerable<Project> GetAllProjs(int cliId)
        {
            return customers.Where(e => e.ClientId == cliId);
        }

        public List<Project> SearchByID(int? ID, string query)
        {
            List<Project> temp = new List<Project>();
            if (ID == null || ID == 0){  return temp;  }

            foreach (Project bluprt in Projects)
            { 
                Project? newproj = Get(bluprt.Id);
                if ((bluprt.ClientId == ID) && (newproj != null)){
                    temp.Add(newproj);
                }
            }
            return temp.Where(s => s.ToString().ToUpper().Contains(query?.ToUpper() ?? string.Empty)).ToList();
        }

        //Make sure to add the clientId as well. Otherwise, it won't work.
        public void Add(Project? Project)
        {
            if (Project != null)
            {
                if (Project.Id == 0) { Project.Id = LastId + 1; }
                customers.Add(Project);
            }
        }

        private int LastId
        {
            get
            {
                return Projects.Any() ? Projects.Select(c => c.Id).Max() : 0;
            }
        }

        public void Read()
        {
            foreach (var money in customers)
            { Console.WriteLine(money); }
            //customers.ForEach(Console.WriteLine);
        }

        public void Edit(Project Model)
        {
            var ProjectToUpdate = Current.GetProj(Model.ClientId, Model.Id);
            if (ProjectToUpdate != null)
            {
                ProjectToUpdate.ShortName = Model.ShortName;
                ProjectToUpdate.LongName = Model.LongName;
            }
        }

        public void Open(Project customerToOpen)
        {
            //Do DateTime.UtcNow for global use
            if (customerToOpen != null)
            {
                customerToOpen.IsActive = true;
                customerToOpen.ClosedDate = DateTime.MaxValue;
            }
        }
        public void Close(Project customerToRemove)
        {
            //Do DateTime.UtcNow for global use
            if (customerToRemove != null)
            {
                customerToRemove.IsActive = false;
                customerToRemove.ClosedDate = DateTime.Now;
            }
        }
        public void CloseAll(int clientId) 
        {
            foreach (var user in customers)
            {
                if (user.ClientId == clientId)
                { user.IsActive = false; }
            }
        }

        public void Delete(Project customerToRemove)
        {
            if (customerToRemove != null)
            { customers.Remove(customerToRemove); }
        }
        public void DeleteAll(int clientId)
        {
            var projIds = customers.Where(c => c.ClientId == clientId).Select(p => p.Id).ToList();
            foreach(var projId in projIds)
            {
                var projToRemove = Projects.FirstOrDefault(p => p.Id == projId);
                if(projToRemove != null)
                {
                    Projects.Remove(projToRemove);
                }
            }
        }
    }
}
