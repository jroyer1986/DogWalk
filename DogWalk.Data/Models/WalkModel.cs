using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogWalk.Data.Models
{
    public class WalkModel
    {
        #region Properties

        public int ID { get; set; }
        public DateTime DateOfWalk { get; set; }
        public WalkStatusModel Status { get; set; }
        public WalkerModel Walker { get; set; }
        public PaymentModel Payment { get; set; }

        #endregion

        #region Constructor

        public WalkModel(int id, DateTime dateOfWalk, WalkStatusModel status, WalkerModel walker, PaymentModel payment)
        {
            ID = id;
            DateOfWalk = dateOfWalk;
            Status = status;
            Walker = walker;
            Payment = payment;
        }

        public WalkModel() { }  

        #endregion

    }
}
