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
    public interface IUserRepository : IEntityRepository<User, int>
    {
        Task<User> GetByID (int Id);
        Task<User> GetUserByEmailAndPassword (string email, string password);
        Task<User> Authenticate (int userId,string token);
    }

    public class UsersRepository : EntityRepositoryBase<User, int>, IUserRepository
    {

        public UsersRepository(DbContext dbContext)
            : base(dbContext)
        { }

        public async Task<User> Authenticate (int userId,string token)
        {
            User user =await this.SingleOrDefaultAsync(u => u.Id == userId && u.Token==token);
            return  (user);
        }

        public async Task<User> GetByID (int Id)
        {
            var item =await this.SingleOrDefaultAsync(p => p.Id == Id) ;
            return item;

        }

        public async Task<User> GetUserByEmailAndPassword (string email, string password)
        {
            User user = await this.SingleOrDefaultAsync(u => u.Email == email && u.Password == password ) ;
            return user;
        }

        //public Dictionary<int, string> ToDictionary()
        //{
        //    var dictionary = this.Get().ToDictionary(r => r.ID, r => r.Title + " " + r.FirstName + " " + r.LastName);
        //    return dictionary;
        //}


    }
}
