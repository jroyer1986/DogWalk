//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DogWalk.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Payment
    {
        public int ID { get; set; }
        public int PaymentStatusID { get; set; }
        public decimal Amount { get; set; }
        public System.DateTime DatePaid { get; set; }
        public int PaymentTypeID { get; set; }
    
        public virtual PaymentStatu PaymentStatu { get; set; }
        public virtual PaymentType PaymentType { get; set; }
    }
}
