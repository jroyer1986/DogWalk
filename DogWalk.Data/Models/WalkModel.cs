using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogWalk.Data.Entities;

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

        public WalkModel(int id, DateTime dateOfWalk, WalkStatusModel status, WalkerModel walker = null, PaymentModel payment = null)
        {
            ID = id;
            DateOfWalk = dateOfWalk;
            Status = status;
            Walker = walker;
            Payment = payment;
        }

        public WalkModel() { }

        public WalkModel(Walk walk)
        {
            //create WalkStatusModel, WalkerModel, and PaymentModel
            WalkStatus walkStatus = walk.WalkStatus;
            WalkStatusModel walkStatusModel = new WalkStatusModel(walkStatus.ID, walkStatus.Status, walkStatus.Explanation);

            Walker walker = walk.Walker;
            WalkerModel walkerModel = new WalkerModel(walker.ID, walker.Name, walker.Phone, walker.Email, walker.Disabled);

            Payment payment = walk.Payment;
            PaymentModel paymentModel = new PaymentModel(payment);

            DateOfWalk = walk.DateOfWalk;
            Status = walkStatusModel;
            Walker = walkerModel;
            Payment = paymentModel;
        }

        #endregion

    }
}
