using FoodMenu.BL;
using FoodMenu.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FoodMenu.WebApi.Filters
{
    public class ApiAuthorizationFilterAttribute :AuthorizationFilterAttribute
    {
        public const string User = "User";
        public const string IsAuthorized = "IsAuthorized";

        public override async Task OnAuthorizationAsync (HttpActionContext actionContext,System.Threading.CancellationToken cancellationToken)
        {
            var allowAnonymous = actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                       || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();

            var userId = actionContext.Request.GetHeader("userId").ToInt();

            var token = actionContext.Request.GetHeader("token");

            //check if this is annonymous action
            if(allowAnonymous)
            {
                return;
            }

            try
            {
                var usersBl = new UsersBL();

                var result = await usersBl.Authenticate(userId,token);

                if(result.Status)
                {
                    actionContext.ActionArguments.Add(User,result.Result);
                }
                else
                {
                    var reason = "שם משתמש או סיסמה לא נכונים";
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = reason };
                    return;
                }
            }

            catch(Exception ex)
            {

            }
        }


        //protected void HandleException (HttpActionContext actionContext,Exception ex,AuthUser user)
        //{
        //    //LogInfo log = new LogInfo() { Message = "BaseAPIController error", Category = Logger.eCategories.UI };
        //    if(user != null)
        //    {
        //        // log.TenantId = user.TenantId;
        //        //log.UserId = user.UserId;
        //    }
        //    //Logger.Instance.LogException(ex, log);
        //    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);//actionContext.Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);

        //}
    }
}