

namespace FoodMenu.Models
{
    public partial class UserModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string Token { get; set; }

        public bool IsActive { get; set; }
        public string LogoFile { get; set; }
        public byte[] LogoFileBytes { get; set; }
        public string BusinessId { get; set; }
        public string Address { get; set; }
    }
}
