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
    
    public partial class Invoice
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Notes { get; set; }
        public int MeetingId { get; set; }
        public string Number { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Meeting Meeting { get; set; }
    }
}
