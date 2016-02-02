using Catel.Data;
using FoodMenu.Models;
using FoodMenu.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMenu.BL
{
    public class UsersBL
    {
        public Task<int> Create ( UserModel userModel )
        {
            return Task.Run(() =>
            {
                using (var session = new UnitOfWork<FoodMenuEntities>())
                {
                    var userRepository = session.GetRepository<IUserRepository>();
                    var user = new User();
                    user.Id = userModel.Id;
                    user.Email = userModel.Email;
                    user.Password = userModel.Password;
                    user.FirstName = userModel.FirstName;
                    user.LastName = userModel.LastName;
                    userRepository.Add(user);

                    session.SaveChanges();
                    return user.Id;
                }
            });
        }

        public Task<List<UserModel>> GetAll ()
        {
            return Task.Run(() =>
            {
                using (var session = new UnitOfWork<FoodMenuEntities>())
                {
                    IUserRepository userRepository = session.GetRepository<IUserRepository>();

                    var userList = userRepository.GetAll().Select(u => new UserModel
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Password = u.Password,
                        FirstName = u.FirstName,
                        LastName = u.LastName
                    }).ToList();

                    return userList;
                }
            });

        }

        public Task<UserModel> GetByID ( int userID )
        {
            return Task.Run(() =>
            {
                UserModel model = new UserModel();
                using (var session = new UnitOfWork<FoodMenuEntities>())
                {
                    var userRepository = session.GetRepository<IUserRepository>();

                    var user = userRepository.GetByID(userID);

                    model = new UserModel()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Password = user.Password,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };

                    return model;
                }
            });
        }

        public Task<bool> Update ( UserModel userModel )
        {
            return Task.Run(() =>
            {
                try
                {
                    using (var session = new UnitOfWork<FoodMenuEntities>())
                    {
                        var userRepository = session.GetRepository<IUserRepository>();

                        var user = userRepository.GetByID(userModel.Id);

                        user.Id = userModel.Id;
                        user.Email = userModel.Email;
                        user.Password = userModel.Password;
                        user.FirstName = userModel.FirstName;
                        user.LastName = userModel.LastName;
                        session.SaveChanges();

                        return true;
                    }
                }
                catch (Exception x)
                {
                    throw x;
                }
            });
        }

        public Task<bool> Delete ( int userID )
        {
            return Task.Run(() =>
            {
                using (var session = new UnitOfWork<FoodMenuEntities>())
                {
                    var userRepository = session.GetRepository<IUserRepository>();

                    var user = userRepository.GetByID(userID);

                    if (user != null)
                    {
                        userRepository.Delete(user);
                        session.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            });

        }

        public Task<UserModel> GetByLogin ( string email, string password )
        {
            return Task.Run(() =>
            {
                UserModel model = new UserModel();
                using (var session = new UnitOfWork<FoodMenuEntities>())
                {

                    var userRepository = session.GetRepository<IUserRepository>();

                    var user = userRepository.GetUserByEmailAndPassword(email, password);

                    if (user == null)
                    {
                        return null;
                    }

                    model = new UserModel()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Password = user.Password,
                        FirstName = user.FirstName,
                        LastName = user.LastName
                    };

                    return model;
                }
            });
        }
    }
}
