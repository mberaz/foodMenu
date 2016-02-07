using FoodMenu.Utils;

namespace FoodMenu.Web.Models
{
    public static class Helpers
    {
        public static string BaseApiUrl ()
        {
            return Utility.AppSetting("api");
        }
    }
}