using Catel.Data;
using Catel.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMenu.Repositories
{
    public interface IInvoiceRepository :IEntityRepository<Invoice,int>
    {
        Task<Invoice> GetByID (int Id);
    }

    public class InvoiceRepository :EntityRepositoryBase<Invoice,int>, IInvoiceRepository
    {

        public InvoiceRepository (DbContext dbContext)
            : base(dbContext)
        { }


        public async Task<Invoice> GetByID (int Id)
        {
            var item = await this.SingleOrDefaultAsync(p => p.Id == Id);
            return item;

        }




        //public Dictionary<int, string> ToDictionary()
        //{
        //    var dictionary = this.Get().ToDictionary(r => r.ID, r => r.Title + " " + r.FirstName + " " + r.LastName);
        //    return dictionary;
        //}


    }
}
