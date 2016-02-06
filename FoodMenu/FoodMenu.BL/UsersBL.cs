using Catel.Data;
using FoodMenu.Models;
using FoodMenu.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodMenu.BL
{
    public class UsersBL
    {
        public async Task<int> Create (UserModel userModel)
        {
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var userRepository = session.GetRepository<IUserRepository>();
                var user = new User();
                user.Id = userModel.Id;
                user.Email = userModel.Email;
                user.Password = userModel.Password;
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.BusinessId = userModel.BusinessId;
                user.Address = userModel.Address;
                userRepository.Add(user);

                await session.SaveChangesAsync();
                return user.Id;
            }
        }

        public Task<List<UserModel>> GetAll ()
        {
            return Task.Run(() =>
            {
                using(var session = new UnitOfWork<FoodMenuEntities>())
                {
                    IUserRepository userRepository = session.GetRepository<IUserRepository>();

                    var userList = userRepository.GetAll().Select(u => new UserModel
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Password = u.Password,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        BusinessId = u.BusinessId,
                        Address = u.Address,
                    }).ToList();

                    return userList;
                }
            });
        }

        public async Task<UserModel> GetByID (int userID)
        {
            UserModel model = new UserModel();
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var userRepository = session.GetRepository<IUserRepository>();

                var user = await userRepository.GetByID(userID);

                model = new UserModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    BusinessId = user.BusinessId,
                    Address = user.Address,
                };

                return model;
            }
        }

        public async Task<bool> Update (UserModel userModel)
        {
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var userRepository = session.GetRepository<IUserRepository>();

                var user = await userRepository.GetByID(userModel.Id);

                user.Id = userModel.Id;
                user.Email = userModel.Email;
                user.Password = userModel.Password;
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.BusinessId = userModel.BusinessId;
                user.Address = userModel.Address;
                await session.SaveChangesAsync();

                return true;
            }
        }

        public async Task<bool> UpdateImage (int userId,string fileName,byte[] bytes)
        {
            try
            {
                using(var session = new UnitOfWork<FoodMenuEntities>())
                {
                    var userRepository = session.GetRepository<IUserRepository>();

                    var user = await userRepository.GetByID(userId);

                    user.LogoFile = fileName;
                    user.LogoFileBytes = bytes;
                    await session.SaveChangesAsync();

                    return true;
                }
            }
            catch(Exception x)
            {
                throw x;
            }
        }

        public async Task<bool> Delete (int userID)
        {
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var userRepository = session.GetRepository<IUserRepository>();

                var user = await userRepository.GetByID(userID);

                if(user != null)
                {
                    user.IsActive = false;
                    await session.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }


        }

        public async Task<UserModel> Authenticate (int userId,string token)
        {
            UserModel model = new UserModel();
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var userRepository = session.GetRepository<IUserRepository>();

                var user = await userRepository.Authenticate(userId,token);

                return user == null ? null : new UserModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = user.Token
                };
            }
        }

        public async Task<bool> Logout (int userId)
        {
            try
            {
                using(var session = new UnitOfWork<FoodMenuEntities>())
                {
                    var userRepository = session.GetRepository<IUserRepository>();

                    var user = await userRepository.GetByID(userId);

                    user.Token = "";
                    await session.SaveChangesAsync();

                    return true;
                }
            }
            catch(Exception x)
            {
                throw x;
            }
        }

        public async Task<UserModel> Login (string email,string password,bool refresh = true)
        {
            UserModel model = new UserModel();
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var userRepository = session.GetRepository<IUserRepository>();

                var user = await userRepository.GetUserByEmailAndPassword(email,password);

                return user == null ? null : new UserModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = refresh ? Guid.NewGuid().ToString() : user.Token
                };

            }
        }
    }
}
