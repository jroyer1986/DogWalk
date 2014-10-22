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

        #endregion

        #region Constructor

        public WalkerModel(int id, string name, string phone, string email)
        {
            ID = id;
            Name = name;
            Phone = phone;
            Email = email;
        }

        public WalkerModel() { }    

        #endregion  
    }
}
