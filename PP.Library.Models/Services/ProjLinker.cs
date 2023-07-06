using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PP_Library.Models;

namespace PP_Library.Services
{
    //Clients are main employers for each project
    //Can add Clients to the Website, which then a
    //Can add Projects to the Website, which then is listed under a Client
    public class ProjLinker
    {
        private static ProjLinker? instance;
        private static object daLock = new object();
        public static ProjLinker Current
        {
            get
            {
                //For thread safety
                lock (daLock)
                {
                    //Critical Part - OS issue if used w/o lock
                    if (instance == null)
                    {
                        instance = new ProjLinker();
                    }
                }

                return instance;
            }
        }

        private List<Client> customers;
        private ProjLinker()
        {
            customers = new List<Client>
            { 
                new Client {Id = 3, Name = "Noob Mckowsky"},
                new Client {Id = 9, Name = "Notisme Chefpie"},
                new Client {Id = 21, Name = "Anub Isme"}
            };
        }
        public List<Client> Clients { get { return customers; } }
        public Client? Get(int id)
        {
            return customers.FirstOrDefault(e => e.Id == id);
        }

        public List<Client> Search(string query)
        {
            return Clients.Where(s => s.Name.ToUpper().Contains(query?.ToUpper() ?? string.Empty)).ToList();
        }

        public void Read()
        {
            foreach (var money in customers)
            { Console.WriteLine(money); }
            //customers.ForEach(Console.WriteLine);
        }

        public void Add(Client? client)
        { 
            if(client != null) 
            {
                if (client.Id == 0) { client.Id = LastId + 1; }
                customers.Add(client);
            }
        }

        private int LastId
        {
            get {  return Clients.Any() ? Clients.Select(c => c.Id).Max() : 0;  }
        }

        public void Edit(Client Model)
        {
            var ClientToUpdate = Current.Get(Model.Id);
            if (ClientToUpdate != null)
            {
                ClientToUpdate.Name = Model.Name;
                ClientToUpdate.Notes = Model.Notes;
            }
        }
        public void CloseClient(Client client) 
        {
            var deactivateTarget = Get(client.Id);
            var Projects = ProjService.Current.GetAllProjs(deactivateTarget?.Id ?? 0);
            var Stuff = Projects.Any(p => p.IsActive); 
            if (deactivateTarget != null && Stuff == false) 
            { deactivateTarget.IsActive = false; }
        }
        public void Delete(int id)
        {
            var customerToRemove = Get(id);
            if (customerToRemove != null) { customers.Remove(customerToRemove); }
        }
        public void Delete(Client c)
        {
            Delete(c.Id);
        }
    }
}
