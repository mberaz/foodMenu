using FoodMenu.BL;
using FoodMenu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FoodMenu.WebApi.Controllers
{
    [RoutePrefix("User")]
    public class UsersController :ApiController
    {
        UsersBL usersBl;
        public UsersController ()
        {
            usersBl = new UsersBL();
        }

        // GET api/values
        [Route("All",Name = "users")]
        [HttpGet]
        public async Task<ReturnModel<List<UserModel>>> Get ()
        {
            return await usersBl.GetAll();
        }

        // GET api/values/5
        [Route("{id}",Name = "user")]
        [HttpGet]
        public async Task<ReturnModel<UserModel>> Get (int id)
        {
            return await usersBl.GetByID(id);
        }

        // POST api/values
        [Route("",Name = "CreateUser")]
        [HttpPost]
        public async Task<ReturnModel<UserModel>> Post (UserModel user)
        {
            return await usersBl.Create(user);
        }

        // PUT api/values/5
        [Route("{id}",Name = "UpdateUser")]
        [HttpPut]
        public async Task<ReturnModel<bool>> Put (int id,UserModel user)
        {
            user.Id = id;
            return await usersBl.Update(user);
        }

        // DELETE api/values/5
        [Route("{id}",Name = "DeleteUser")]
        [HttpDelete]
        public async Task<ReturnModel<bool>> Delete (int id)
        {
            return await usersBl.Delete(id);
        }
    }
}
