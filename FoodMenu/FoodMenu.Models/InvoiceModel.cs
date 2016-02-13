

namespace FoodMenu.Models
{
    public class InvoiceModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Notes { get; set; }
        public int MeetingId { get; set; }
        public string Number { get; set; }
    }
}
