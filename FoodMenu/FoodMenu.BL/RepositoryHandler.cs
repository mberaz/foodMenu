using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMenu.BL
{
   public class RepositoryHandler
    {
        public static void Init ()
        {
            RepositoryUtils.SetDbContextType();
        }
    }
}
