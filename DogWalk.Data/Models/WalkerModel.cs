using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogWalk.Data.Models
{
    public class WalkerModel
    {
        #region Properties

        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool Disabled { get; set; }  

        #endregion

        #region Constructor

        public WalkerModel(int id, string name, string phone, string email, bool disabled = false)
        {
            ID = id;
            Name = name;
            Phone = phone;
            Email = email;
            Disabled = disabled;
        }

        public WalkerModel() { }    

        #endregion  
    }
}
