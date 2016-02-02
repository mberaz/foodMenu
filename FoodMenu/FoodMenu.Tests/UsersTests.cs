using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using FoodMenu.BL;
using FoodMenu.Models;

namespace FoodMenu.Tests
{
    [TestClass]
    public class UsersTests
    {
        public static UsersBL usersBL;
        [ClassInitialize]
        public static void Init ( TestContext test )
        {
            RepositoryUtils.SetDbContextType();
            usersBL = new UsersBL();

        }


        [TestMethod]
        public async Task InserUser_OK ()
        {
            var user = new UserModel();

            var ticks = DateTime.Now.Ticks.ToString().Substring(6);
            user.Email = $"mbearz{ticks}@gmail.com";
            user.Password = "123456";
            user.FirstName = "michael";
            user.LastName = "berezin";

            var id = await usersBL.Create(user);
            Assert.AreNotEqual(id,0);
        }

        [TestMethod]
        public async Task GetUser_OK ()
        {
            var user = await usersBL.GetByID(1);
            Assert.AreNotEqual(user,null);
        }
    }
}
