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
    
    public partial class PaymentStatu
    {
        public PaymentStatu()
        {
            this.Payments = new HashSet<Payment>();
        }
    
        public int ID { get; set; }
        public string Status { get; set; }
        public string Explanation { get; set; }
    
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
