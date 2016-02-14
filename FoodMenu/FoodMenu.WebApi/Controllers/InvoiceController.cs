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
    [RoutePrefix("Invoice")]
    public class InvoiceController :BaseApiController
    {
        InvoiceBL InvoiceBl;
        public InvoiceController ()
        {
            InvoiceBl = new InvoiceBL();
        }

        [Route("table",Name = "InvoiceTable")]
        [HttpGet]
        public async Task<FormatedList<InvoiceModel>> GetTable (int clientId)
        {
            try
            {
                var result = await InvoiceBl.GetAll(Request.GetQueryStrings().ToNameValueCollection(),clientId);
                return result;
            }
            catch
            {
                return null;
            }
        }

        // GET api/values/5
        [Route("{id}",Name = "Invoice")]
        [HttpGet]
        public async Task<ReturnModel<InvoiceModel>> Get (int id)
        {
            return await InvoiceBl.GetByID(id);
        }

        // POST api/values
        [Route("",Name = "CreateInvoice")]
        [HttpPost]
        //[AllowAnonymous]
        public async Task<ReturnModel<InvoiceModel>> Post (InvoiceModel Invoice)
        {
            Invoice.Id = LogedInUser.Id;
            return await InvoiceBl.Create(Invoice);
        }

        // PUT api/values/5
        [Route("{id}",Name = "UpdateInvoice")]
        [HttpPut]
        public async Task<ReturnModel<bool>> Put (int id,InvoiceModel Invoice)
        {
            Invoice.Id = id;
            return await InvoiceBl.Update(Invoice);
        }


    }
}
