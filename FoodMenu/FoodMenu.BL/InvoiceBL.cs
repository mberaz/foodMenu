using Catel.Data;
using DataTablesParser;
using FoodMenu.Models;
using FoodMenu.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMenu.BL
{
    public class InvoiceBL
    {
        public async Task<ReturnModel<InvoiceModel>> Create (InvoiceModel invoiceModel)
        {
            var result = new ReturnModel<InvoiceModel> { Status = true };
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var InvoiceRepository = session.GetRepository<IInvoiceRepository>();

                var invoice = new Invoice();
                invoice.Id = invoiceModel.Id;
                invoice.ClientId = invoiceModel.ClientId;
                invoice.Notes = invoiceModel.Notes;
                invoice.MeetingId = invoiceModel.MeetingId;
                invoice.Number = invoiceModel.Number;
                InvoiceRepository.Add(invoice);

                await session.SaveChangesAsync();

                invoiceModel.Id = invoice.Id;
                result.Result = invoiceModel;
                return result;
            }
        }

        public Task<FormatedList<InvoiceModel>> GetAll (NameValueCollection requestParams,int clientId)
        {
            return Task.Run(() =>
            {
                using(var session = new UnitOfWork<FoodMenuEntities>())
                {
                    IInvoiceRepository InvoiceRepository = session.GetRepository<IInvoiceRepository>();

                    var InvoiceList = InvoiceRepository.GetAll().Where(u => u.ClientId == clientId).Select(i => new InvoiceModel
                    {
                        Id = i.Id,
                        ClientId = i.ClientId,
                        Notes = i.Notes,
                        MeetingId = i.MeetingId,
                        Number = i.Number,
                    });

                    var parser = new DataTableEntityParser<InvoiceModel>(requestParams,InvoiceList.AsQueryable());

                    var list = parser.Parse();

                    return list;
                }
            });
        }

        public async Task<ReturnModel<InvoiceModel>> GetByID (int invoiceID)
        {
            InvoiceModel invoiceModel = new InvoiceModel();
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var InvoiceRepository = session.GetRepository<IInvoiceRepository>();

                var invoice = await InvoiceRepository.GetByID(invoiceID);

                invoiceModel.Id = invoice.Id;
                invoiceModel.ClientId = invoice.ClientId;
                invoiceModel.Notes = invoice.Notes;
                invoiceModel.MeetingId = invoice.MeetingId;
                invoiceModel.Number = invoice.Number;

                return new ReturnModel<InvoiceModel>
                {
                    Status = true,
                    Result = invoiceModel
                };
            };
        }

        public async Task<ReturnModel<bool>> Update (InvoiceModel invoiceModel)
        {
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var InvoiceRepository = session.GetRepository<IInvoiceRepository>();

                var invoice = await InvoiceRepository.GetByID(invoiceModel.Id);

                invoice.Id = invoiceModel.Id;
                invoice.ClientId = invoiceModel.ClientId;
                invoice.Notes = invoiceModel.Notes;
                invoice.MeetingId = invoiceModel.MeetingId;
                invoice.Number = invoiceModel.Number;
                await session.SaveChangesAsync();

                return new ReturnModel<bool> { Status = true };
            }
        }
    }
}
