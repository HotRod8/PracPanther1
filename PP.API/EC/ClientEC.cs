using PP.API.Database;
using PP.API.Messengers;
using PP_Library.DTO;
using PP_Library.Models;

namespace PP.API.EC
{
    public class ClientEC
    {
        //Double-check this one.
        public ClientDTO AddOrUpdate(ClientDTO dto)
        {
            //if (dto.Id > 0)
            //{
            //    var clientToUpdate
            //        = Filebase.Current.Clients
            //        .FirstOrDefault(c => c.Id == dto.Id);
            //    if (clientToUpdate != null)
            //    {
            //        Filebase.Current.Clients.Remove(clientToUpdate);
            //    }
            //    Filebase.Current.Clients.Add(new Client(dto));
            //}
            //else
            //{
            //    dto.Id = Filebase.LastClientId + 1;
            //    Filebase.Current.Clients.Add(new Client(dto));
            //}

            var client = Filebase.Current.AddOrUpdate(new Client(dto));

            return new ClientDTO(client);
        }

        //Connected
        public ClientDTO? Get(int id)
        {
            var returnVal = Filebase.Current.Clients
                 .FirstOrDefault(c => c.Id == id)
                 ?? new Client();

            return new ClientDTO(returnVal);
        }

        //Connected
        public ClientDTO? Delete(int id)
        {
            string ID = string.Empty + id;
            if (Filebase.Current.Delete(ID) == true)
            {
                return new ClientDTO();
            }
            return null;
        }

        //Connected
        public IEnumerable<ClientDTO> Search(string query = "")
        {
            return Filebase.Current.Clients.
                Where(c => c.Name.ToUpper()
                    .Contains(query.ToUpper()))
                .Take(1000)
                .Select(c => new ClientDTO(c));
        }
    }
}