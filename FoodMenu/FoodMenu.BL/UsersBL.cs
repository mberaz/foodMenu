using Catel.Data;
using DataTablesParser;
using FoodMenu.Models;
using FoodMenu.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace FoodMenu.BL
{
    public class UsersBL
    {
        public async Task<ReturnModel<UserModel>> Create (UserModel userModel)
        {
            var result = new ReturnModel<UserModel> { Status = true };
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var userRepository = session.GetRepository<IUserRepository>();

                if(!(await userRepository.ValidateEmail(userModel.Email,userModel.Id)))
                {
                    result.Error = ("כתובת האמייל כבר בשימוש.");
                    result.Status = false;
                    return result;
                }

                var user = new User();
                user.Id = userModel.Id;
                user.Email = userModel.Email;
                user.Password = userModel.Password;
                user.FirstName = userModel.FirstName;
                user.LastName = userModel.LastName;
                user.BusinessId = userModel.BusinessId;
                user.Address = userModel.Address;
                user.IsActive = true;
                userRepository.Add(user);

                await session.SaveChangesAsync();

                userModel.Id = user.Id;
                userModel.Token = Guid.NewGuid().ToString();
                result.Result = userModel;
                return result;
            }
        }



        public Task<FormatedList<UserModel>> GetAll (NameValueCollection requestParams)
        {
            return Task.Run(() =>
            {
                using(var session = new UnitOfWork<FoodMenuEntities>())
                {
                    IUserRepository userRepository = session.GetRepository<IUserRepository>();

                    var userList = userRepository.GetAll().Where(u => u.IsActive).Select(u => new UserModel
                    {
                        Id = u.Id,
                        Email = u.Email,
                        Password = u.Password,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        BusinessId = u.BusinessId,
                        Address = u.Address,
                    });

                    var parser = new DataTableEntityParser<UserModel>(requestParams,userList.AsQueryable());

                    return parser.Parse();
                }
            });
        }

        public Task<ReturnModel<List<UserModel>>> GetAll ()
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

                    return new ReturnModel<List<UserModel>> { Status = true,Result = userList };
                }
            });
        }

        public async Task<ReturnModel<UserModel>> GetByID (int userID)
        {
            UserModel model = new UserModel();
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var userRepository = session.GetRepository<IUserRepository>();

                var user = await userRepository.GetByID(userID);

                return new ReturnModel<UserModel>
                {
                    Status = true,
                    Result = new UserModel()
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Password = user.Password,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        BusinessId = user.BusinessId,
                        Address = user.Address,
                    }
                };
            };
        }


        public async Task<ReturnModel<bool>> Update (UserModel userModel)
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

                return new ReturnModel<bool> { Status = true };
            }
        }

        public async Task<ReturnModel<bool>> UpdateImage (int userId,string fileName,byte[] bytes)
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

                    return new ReturnModel<bool> { Status = true };
                }
            }
            catch(Exception x)
            {
                throw x;
            }
        }

        public async Task<ReturnModel<FileModel>> GetLogo (int userId)
        {
            try
            {
                using(var session = new UnitOfWork<FoodMenuEntities>())
                {
                    var userRepository = session.GetRepository<IUserRepository>();

                    var user = await userRepository.GetByID(userId);

                    return new ReturnModel<FileModel>
                    {
                        Status = true,
                        Result = new FileModel
                        {
                            Name = user.LogoFile,
                            Bytes = user.LogoFileBytes
                        }

                    };
                }
            }
            catch(Exception x)
            {
                throw x;
            }
        }

        public async Task<ReturnModel<bool>> Delete (int userID)
        {
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var userRepository = session.GetRepository<IUserRepository>();

                var user = await userRepository.GetByID(userID);

                if(user != null)
                {
                    user.IsActive = false;
                    await session.SaveChangesAsync();
                    return new ReturnModel<bool> { Status = true };
                }
                else
                {
                    return new ReturnModel<bool> { Status = false };
                }


            }


        }

        public async Task<ReturnModel<UserModel>> Authenticate (int userId,string token)
        {
            var res = new ReturnModel<UserModel>();
            UserModel model = new UserModel();
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var userRepository = session.GetRepository<IUserRepository>();

                var user = await userRepository.Authenticate(userId,token);

                res.Result = user == null ? null : new UserModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = user.Token
                };
                res.Status = user != null;
                return res;
            }
        }

        public async Task<ReturnModel<bool>> Logout (int userId)
        {
            try
            {
                using(var session = new UnitOfWork<FoodMenuEntities>())
                {
                    var userRepository = session.GetRepository<IUserRepository>();

                    var user = await userRepository.GetByID(userId);

                    user.Token = "";
                    await session.SaveChangesAsync();

                    return new ReturnModel<bool> { Status = true };
                }
            }
            catch(Exception x)
            {
                throw x;
            }
        }

        public async Task<ReturnModel<UserModel>> Login (string email,string password,bool refresh = true)
        {
            UserModel model = new UserModel();
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var userRepository = session.GetRepository<IUserRepository>();

                var user = await userRepository.GetUserByEmailAndPassword(email,password);

                var token = refresh ? Guid.NewGuid().ToString() : user.Token;

                user.Token = token;
                await session.SaveChangesAsync();

                var result = new ReturnModel<UserModel>();
                result.Result = user == null ? null : new UserModel()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Password = user.Password,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = token
                };

                result.Status = user != null;
                result.Error = "שם משתמש או סיסמה לא נכונים";

                return result;
            }
        }
    }
}
