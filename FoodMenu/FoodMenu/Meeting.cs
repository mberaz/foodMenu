//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FoodMenu
{
    using System;
    using System.Collections.Generic;
    
    public partial class Meeting
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Meeting()
        {
            this.Invoices = new HashSet<Invoice>();
        }
    
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
    
        public virtual Client Client { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
