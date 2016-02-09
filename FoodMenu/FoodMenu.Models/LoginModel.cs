using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodMenu.Models
{
    public class LoginModel
    {
        public string Redirect { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
