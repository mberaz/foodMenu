using Catel.IoC;
using FoodMenu.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMenu
{
    public class RepositoryUtils
    {
        public static void SetDbContextType ()
        {
            var serviceLocator = ServiceLocator.Default;

            serviceLocator.RegisterType<IUserRepository, UsersRepository>();
            serviceLocator.RegisterType<IClientRepository,ClientRepository>();
            serviceLocator.RegisterType<IMeetingRepository,MeetingRepository>();
            serviceLocator.RegisterType<IInvoiceRepository,InvoiceRepository>();
            //serviceLocator.RegisterType<IKnowledgeRepository, KnowledgeRepository>();
            //serviceLocator.RegisterType<ITaskRunsRepository, TaskRunsRepository>();
            //serviceLocator.RegisterType<ITasksRepository, TasksRepository>();

        }
    }
}
