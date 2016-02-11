using Catel.Data;
using DataTablesParser;
using FoodMenu;
using FoodMenu.Models;
using FoodMenu.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace FoodMenu.BL
{
    public class ClientsBL
    {
        public async Task<ReturnModel<ClientModel>> Create (ClientModel ClientModel)
        {
            var result = new ReturnModel<ClientModel> { Status = true };
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var ClientRepository = session.GetRepository<IClientRepository>();

                var client = new Client();
                client.Id = ClientModel.Id;
                client.Name = ClientModel.Name;
                client.Email = ClientModel.Email;
                client.Phone = ClientModel.Phone;
                client.Nationalid = ClientModel.Nationalid;
                client.Sex = ClientModel.Sex;
                client.Height = ClientModel.Height;
                client.Weight = ClientModel.Weight;
                client.FatPercentage = ClientModel.FatPercentage;
                client.Goal = ClientModel.Goal;
                client.Pills = ClientModel.Pills;
                client.Supplement = ClientModel.Supplement;
                client.RedirectedBy = ClientModel.RedirectedBy;
                client.Price = ClientModel.Price;
                client.RMR = ClientModel.RMR;
                client.UserId = ClientModel.UserId;
                ClientRepository.Add(client);

                await session.SaveChangesAsync();

                ClientModel.Id = client.Id;
                result.Result = ClientModel;
                return result;
            }
        }

        public Task<FormatedList<ClientModel>> GetAll (NameValueCollection requestParams)
        {
            return Task.Run(() =>
            {
                using(var session = new UnitOfWork<FoodMenuEntities>())
                {
                    IClientRepository ClientRepository = session.GetRepository<IClientRepository>();

                    var ClientList = ClientRepository.GetAll().Where(u => u.IsActive).Select(c => new ClientModel
                    {
                        Id = c.Id,
                        Name = c.Name,
                        Email = c.Email,
                        Phone = c.Phone,
                        Nationalid = c.Nationalid,
                        Sex = c.Sex,
                        Height = c.Height,
                        Weight = c.Weight,
                        FatPercentage = c.FatPercentage,
                        Goal = c.Goal,
                        Pills = c.Pills,
                        Supplement = c.Supplement,
                        RedirectedBy = c.RedirectedBy,
                        Price = c.Price,
                        RMR = c.RMR,
                        UserId = c.UserId,
                    });

                    var parser = new DataTableEntityParser<ClientModel>(requestParams,ClientList.AsQueryable());

                    return parser.Parse();
                }
            });
        }

        public async Task<ReturnModel<ClientModel>> GetByID (int ClientID)
        {
            ClientModel clientModel = new ClientModel();
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var ClientRepository = session.GetRepository<IClientRepository>();

                var client = await ClientRepository.GetByID(ClientID);

                clientModel.Id = client.Id;
                clientModel.Name = client.Name;
                clientModel.Email = client.Email;
                clientModel.Phone = client.Phone;
                clientModel.Nationalid = client.Nationalid;
                clientModel.Sex = client.Sex;
                clientModel.Height = client.Height;
                clientModel.Weight = client.Weight;
                clientModel.FatPercentage = client.FatPercentage;
                clientModel.Goal = client.Goal;
                clientModel.Pills = client.Pills;
                clientModel.Supplement = client.Supplement;
                clientModel.RedirectedBy = client.RedirectedBy;
                clientModel.Price = client.Price;
                clientModel.RMR = client.RMR;
                clientModel.UserId = client.UserId;
                clientModel.UserName = client.User.FirstName + " " + client.User.LastName;

                return new ReturnModel<ClientModel>
                {
                    Status = true,
                    Result = clientModel
                };
            };
        }

        public async Task<ReturnModel<bool>> Update (ClientModel ClientModel)
        {
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var ClientRepository = session.GetRepository<IClientRepository>();

                var client = await ClientRepository.GetByID(ClientModel.Id);

                client.Id = ClientModel.Id;
                client.Name = ClientModel.Name;
                client.Email = ClientModel.Email;
                client.Phone = ClientModel.Phone;
                client.Nationalid = ClientModel.Nationalid;
                client.Sex = ClientModel.Sex;
                client.Height = ClientModel.Height;
                client.Weight = ClientModel.Weight;
                client.FatPercentage = ClientModel.FatPercentage;
                client.Goal = ClientModel.Goal;
                client.Pills = ClientModel.Pills;
                client.Supplement = ClientModel.Supplement;
                client.RedirectedBy = ClientModel.RedirectedBy;
                client.Price = ClientModel.Price;
                client.RMR = ClientModel.RMR;
                client.UserId = ClientModel.UserId;
                await session.SaveChangesAsync();

                return new ReturnModel<bool> { Status = true };
            }
        }

        public async Task<ReturnModel<bool>> Delete (int ClientID)
        {
            using(var session = new UnitOfWork<FoodMenuEntities>())
            {
                var ClientRepository = session.GetRepository<IClientRepository>();

                var client = await ClientRepository.GetByID(ClientID);

                if(client != null)
                {
                    client.IsActive = false;
                    await session.SaveChangesAsync();
                    return new ReturnModel<bool> { Status = true };
                }
                else
                {
                    return new ReturnModel<bool> { Status = false };
                }


            }


        }


    }
}
