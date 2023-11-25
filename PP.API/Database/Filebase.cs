using Newtonsoft.Json;
using PP.API.EC;
using PP_Library.Models;

namespace PP.API.Database
{
    public class Filebase
    {
        private string _root;
        private string _clientRoot;
        private string _projectRoot;
        private DirectoryInfo? _rootModel;
        private static Filebase? _instance;


        public static Filebase Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private Filebase()
        {
            //"C:\\temp"
            _root = @"C:\temp";
            _clientRoot = $"{_root}\\Clients";
            _projectRoot = $"{_root}\\Projects";
            _rootModel = null;
            //TODO: add support for employees, times, bills
        }
        private int LastClientId => Clients.Any() ? Clients.Select(c => c.Id).Max() : 0;
        public Client AddOrUpdate(Client c)
        {
            //set up a new Id if one doesn't already exist
            if (c.Id <= 0)
            {
                c.Id = LastClientId + 1;
            }

            var path = $"{_clientRoot}\\{c.Id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(c));

            //return the item, which now has an id
            return c;
        }

        //For running something like todo, click the view button when hovering over the
        //JsonConvert.DeserializeObject<Client> to debug it for issues
        public List<Client> Clients
        {
            get
            {
                _rootModel = new DirectoryInfo(_clientRoot);
                var _clients = new List<Client>();
                if(_rootModel.Exists == true)
                {
                    foreach (var todoFile in _rootModel.GetFiles())
                    {
                        var todo = JsonConvert.
                            DeserializeObject<Client>
                            (File.ReadAllText(todoFile.FullName));
                        if (todo != null)
                        {
                            _clients.Add(todo);
                        }
                    }
                }
                else { _rootModel.Create(); }
                return _clients;
            }
        }

        public bool Delete(string id)
        {
            var path = $"{_clientRoot}\\{id}.json";

            //if the item has been previously persisted
            if (File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }
            return true;
        }
    }

}
