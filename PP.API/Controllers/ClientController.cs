using Microsoft.AspNetCore.Mvc;
using PP.API.Database;
using PP.API.Messengers;
using PP_Library.Models;
using PP_Library.DTO;
using PP.API.EC;
using PP_Library.Utilities; //this is correct - just needs a reboot

namespace PP.API.Controllers
{
    //decorators below
    [ApiController]
    [Route("[controller]")]
    //This route gives a controller for everything
    public class ClientController : ControllerBase
    {
        //Call upon the routes from ProjLinker to effect the Filebase
        private readonly ILogger<ClientController> _logger;

        public ClientController(ILogger<ClientController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<ClientDTO> Get()
        {
            return new ClientEC().Search();
        }

        [HttpGet("GetClients/{id}")]
        public ClientDTO? GetId(int id)
        {
            return new ClientEC().Get(id);
        }

        [HttpDelete("Delete/{id}")]
        public ClientDTO? Delete(int id)
        {
            return new ClientEC().Delete(id);
        }

        //For my case, make a messenger class to pass in multiple parameters at once.
        [HttpPost("{id}")]
        public ClientDTO AddOrUpdate([FromBody] ClientDTO client)
        {
            return new ClientEC().AddOrUpdate(client);
        }

        //Technical Interview Question: If a user is adding a client and
        //they don't understand that we are updating the list to the best
        //degree so far, how do we ensure that it doesn't take long for them?
        [HttpPost("Search")]
        public IEnumerable<ClientDTO> Search([FromBody] QueryMessage query)
        {
            return new ClientEC().Search(query.Query);
        }
        //Answer: Make a manual refresh button for them and let the server side
        //adjust for
    }
}
