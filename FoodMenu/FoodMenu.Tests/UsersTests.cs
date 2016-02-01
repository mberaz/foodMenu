using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catel.Data;
using FoodMenu.Repositories;


namespace FoodMenu.Tests
{
    [TestClass]
    public class UsersTests
    {
        [ClassInitialize]
        public static void Init(TestContext test)
        {
            FoodMenu.RepositoryUtils.SetDbContextType();

        }

        [TestMethod]
        public void InserUser()
        {
            using (var session = new UnitOfWork<FoodMenuEntities>())
            {
                var userRepository = session.GetRepository<IUserRepository>();
                var user = new User();
                user.Id = 1;
                user.Email = "mbearz@gmail.com";
                user.Password = "123456";
                user.FirstName = "michael";
                user.LastName = "berezin";
                userRepository.Add(user);

                session.SaveChanges();


                Assert.AreNotEqual(user.Id,0);
            }
        }
    }
}
