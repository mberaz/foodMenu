

namespace FoodMenu.Models
{
    public class MeetingModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public System.DateTime Date { get; set; }
        public int Weight { get; set; }
        public int Water { get; set; }
        public int Muscle { get; set; }
        public int FatPercentage { get; set; }
        public int MeetingIndex { get; set; }
        public int ArmMuscleMeasurement { get; set; }
        public int ArmNOMuscleMeasurement { get; set; }
        public int HipMeasurement { get; set; }
        public int StomachMeasurement { get; set; }
        public int ThighMeasurement { get; set; }
        public int FrontHandFat { get; set; }
        public int BackHandFat { get; set; }
        public int ThighFat { get; set; }
        public int BackFat { get; set; }
        public int FatAvrg { get; set; }
    }
}
