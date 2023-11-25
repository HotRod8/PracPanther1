using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using PP_Library.Models;
﻿using Newtonsoft.Json;
﻿using PP_Library.DTO;
﻿using PP_Library.Utilities;


namespace PP_Library.Services
{
    //Clients are main employers for each project
    //Can add Clients to the Website, which then a
    //Can add Projects to the Website, which then is listed under a Client
    public class ProjLinker
    {
        private List<ClientDTO> clients;
        public List<ClientDTO> Clients { 
            get {
                
                return clients ?? new List<ClientDTO>();
            } 
        }

        //private List<Client> clients;

        private static ProjLinker? instance;

        public static ProjLinker Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ProjLinker();
                }
                return instance;
            }
        }
        private ProjLinker()
        {
            var response = new WebRequestHandler()
                    .Get("/Client")
                    .Result;

            clients = JsonConvert
                .DeserializeObject<List<ClientDTO>>(response)
                ?? new List<ClientDTO>();
        }

        public void Delete(ClientDTO c)
        {
            var response = new WebRequestHandler()
                    .Delete($"/Client/Delete/{c.Id}").Result;
            Clients.Remove(c);
        }

        public void AddOrUpdate(ClientDTO c)
        {
            //Sends c to the Database to update it or add to it.
            var response = new WebRequestHandler().Post($"/Client/{c.Id}", c).Result;
            
            //The rest handles the client-side display
            var myUpdatedClient = JsonConvert.DeserializeObject<ClientDTO>(response);
            if(myUpdatedClient != null)
            {
                var existingClient = clients.FirstOrDefault(c => c.Id == myUpdatedClient.Id);
                if(existingClient == null)
                {
                    clients.Add(myUpdatedClient);
                }else
                {
                    var index = clients.IndexOf(existingClient);
                    clients.RemoveAt(index);
                    clients.Insert(index, myUpdatedClient);
                }
            }

        }

        //Adjust this to delete from backside table
        public void CloseClient(ClientDTO client)
        {
            var deactivateTarget = Get(client.Id);
            var Projects = ProjService.Current.GetAllProjs(deactivateTarget?.Id ?? 0);
            var Stuff = Projects.Any(p => p.IsActive);
            if (deactivateTarget != null && Stuff == false)
            { deactivateTarget.IsActive = false; }
        }

        public ClientDTO? Get(int id)
        {
            var response = new WebRequestHandler()
                    .Get($"/Client/GetClients/{id}")
                    .Result;
            var client = JsonConvert.DeserializeObject<Client>(response);
            return Clients.FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<ClientDTO> Search(string query)
        {
            return Clients
                .Where(c => c.ToString().ToUpper()
                    .Contains(query.ToUpper()));
        }
    }

}
