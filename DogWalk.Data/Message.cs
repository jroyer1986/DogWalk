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
    
    public partial class Message
    {
        public int ID { get; set; }
        public int WalkerID { get; set; }
        public string Body { get; set; }
        public System.DateTime DateSent { get; set; }
        public int ContactMethodID { get; set; }
    
        public virtual ContactMethod ContactMethod { get; set; }
    }
}
