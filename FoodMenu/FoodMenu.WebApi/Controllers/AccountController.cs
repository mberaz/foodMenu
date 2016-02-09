
using FoodMenu.BL;
using FoodMenu.Models;
using FoodMenu.WebApi.Filters;
using System.Threading.Tasks;
using System.Web.Http;

namespace FoodMenu.WebApi.Controllers
{
    [RoutePrefix("Account")]
    [AllowAnonymous]
    public class AccountController : ApiController
    {
        UsersBL usersBl;
        public AccountController ()
        {
            usersBl = new UsersBL();
        }

        // GET api/values
        [Route("Login",Name = "Login")]
        [HttpPost]
        public async Task<ReturnModel< UserModel>> Login (UserModel model)
        {
            return await usersBl.Login(model.Email,model.Password);
        }
    }
}
