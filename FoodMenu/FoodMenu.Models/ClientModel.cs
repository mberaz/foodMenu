

namespace FoodMenu.Models
{
    public class ClientModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Nationalid { get; set; }
        public string Sex { get; set; }
        public int Height { get; set; }
        public double Weight { get; set; }
        public int FatPercentage { get; set; }
        public string Goal { get; set; }
        public string Pills { get; set; }
        public string Supplement { get; set; }
        public string RedirectedBy { get; set; }
        public int Price { get; set; }
        public int RMR { get; set; }
      
        public bool IsActive { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
