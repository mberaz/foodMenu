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
    [RoutePrefix("Meeting")]
    public class MeetingController :BaseApiController
    {
       MeetingBL MeetingBl;
        public MeetingController ()
        {
           MeetingBl = new MeetingBL();
        }

        [Route("table",Name = "MeetingTable")]
        [HttpGet]
        public async Task<FormatedList<MeetingModel>> GetTable (int MeetingId)
        {
            try
            {
                var result = await MeetingBl.GetAll(Request.GetQueryStrings().ToNameValueCollection(),MeetingId);
                return result;
            }
            catch
            {
                return null;
            }
        }

        // GET api/values/5
        [Route("{id}",Name = "Meeting")]
        [HttpGet]
        public async Task<ReturnModel<MeetingModel>> Get (int id)
        {
            return await MeetingBl.GetByID(id);
        }

        // POST api/values
        [Route("",Name = "CreateMeeting")]
        [HttpPost]
        //[AllowAnonymous]
        public async Task<ReturnModel<MeetingModel>> Post (MeetingModel Meeting)
        {
           Meeting.Id = LogedInUser.Id;
            return await MeetingBl.Create(Meeting);
        }

        // PUT api/values/5
        [Route("{id}",Name = "UpdateMeeting")]
        [HttpPut]
        public async Task<ReturnModel<bool>> Put (int id,MeetingModel Meeting)
        {
           Meeting.Id = id;
            return await MeetingBl.Update(Meeting);
        }

        
    }
}
