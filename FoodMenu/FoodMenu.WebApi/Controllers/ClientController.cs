using DataTablesParser;
using FoodMenu.BL;
using FoodMenu.Models;
using FoodMenu.Utils;
using FoodMenu.WebApi.Filters;
using System.Threading.Tasks;
using System.Web.Http;

namespace FoodMenu.WebApi.Controllers
{
    [ApiAuthorizationFilter]
    [RoutePrefix("Client")]
    public class ClientController :BaseApiController
    {
        ClientsBL ClientBl;
        public ClientController ()
        {
            ClientBl = new ClientsBL();
        }

        [Route("table",Name = "ClientTable")]
        [HttpGet]
        public async Task<FormatedList<ClientModel>> GetTable ()
        {
            try
            {
                var result = await ClientBl.GetAll(Request.GetQueryStrings().ToNameValueCollection());
                return result;
            }
            catch
            {
                return null;
            }
        }

        // GET api/values/5
        [Route("{id}",Name = "client")]
        [HttpGet]
        public async Task<ReturnModel<ClientModel>> Get (int id)
        {
            return await ClientBl.GetByID(id);
        }

        // POST api/values
        [Route("",Name = "CreateClient")]
        [HttpPost]
        //[AllowAnonymous]
        public async Task<ReturnModel<ClientModel>> Post (ClientModel client)
        {
            client.Id = LogedInUser.Id;
            return await ClientBl.Create(client);
        }

        // PUT api/values/5
        [Route("{id}",Name = "UpdateClient")]
        [HttpPut]
        public async Task<ReturnModel<bool>> Put (int id,ClientModel client)
        {
            client.Id = id;
            return await ClientBl.Update(client);
        }

        // DELETE api/values/5
        [Route("{id}",Name = "DeleteClient")]
        [HttpDelete]
        public async Task<ReturnModel<bool>> Delete (int id)
        {
            return await ClientBl.Delete(id);
        }
    }
}
