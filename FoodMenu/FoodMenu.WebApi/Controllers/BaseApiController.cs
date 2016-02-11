using FoodMenu.Models;
using FoodMenu.WebApi.Filters;
using System;

using System.Web.Http;

namespace FoodMenu.WebApi.Controllers
{
    public class BaseApiController :ApiController
    {

        private UserModel _logedInUser;
        protected UserModel LogedInUser
        {
            get
            {
                if(_logedInUser == null)
                {
                    var authUser = (UserModel)ActionContext.ActionArguments[ApiAuthorizationFilterAttribute.User];
                    return authUser;
                }
                return _logedInUser;

            }
        }

    }
}
