using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PP_Library.Models;

namespace PP_Library.Services
{
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
            customers = new List<Client>();
        }

        public Client? Get(int id)
        {
            return customers.FirstOrDefault(e => e.Id == id);
        }

        public void Add(Client? client)
        { 
            if(client != null) { customers.Add(client); }
        }

        public void Read()
        {
            foreach (var money in customers)
            { Console.WriteLine(money); }
            //customers.ForEach(Console.WriteLine);
        }
        public void Delete(int id)
        {
            var customerToRemove = Get(id);
            if (customerToRemove != null) { customers.Remove(customerToRemove); }
        }
    }
}
