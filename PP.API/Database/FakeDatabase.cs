using PP_Library.Models;

namespace PP.API.Database
{
    public static class FakeDatabase
    {
        public static List<Client> Clients = new List<Client>
        {
            new Client {Id = 3, Name = "Noob Mckowsky"},
            new Client {Id = 9, Name = "Notisme Chefpie"},
            new Client {Id = 21, Name = "Anub Isme"}
        };
        public static int LastClientId
        => Clients.Any() ? Clients.Select(c => c.Id).Max() : 0;
    }
}
