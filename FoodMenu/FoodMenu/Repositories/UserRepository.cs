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
        User GetByID(int Id);
        User GetUserByEmailAndPassword(string email, string password);
    }

    public class UsersRepository : EntityRepositoryBase<User, int>, IUserRepository
    {

        public UsersRepository(DbContext dbContext)
            : base(dbContext)
        { }


        public User GetByID(int Id)
        {
            var item = this.Find(p => p.Id == Id).SingleOrDefault();
            return item;

        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            User user = this.Find(u => u.Email == email && u.Password == password ).SingleOrDefault();
            return user;
        }

        //public Dictionary<int, string> ToDictionary()
        //{
        //    var dictionary = this.Get().ToDictionary(r => r.ID, r => r.Title + " " + r.FirstName + " " + r.LastName);
        //    return dictionary;
        //}


    }
}
