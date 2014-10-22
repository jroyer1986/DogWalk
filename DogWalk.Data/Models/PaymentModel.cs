using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogWalk.Data.Models
{
    public class PaymentModel
    {
        #region Properties

        public int ID { get; set; }
        public PaymentStatusModel PaymentStatus { get; set; }
        public decimal Amount { get; set; }
        public DateTime DatePaid { get; set; }
        public PaymentTypeModel PaymentType { get; set; }

        #endregion

        #region Constructor

        public PaymentModel(int id, PaymentStatusModel paymentStatus, decimal amount, DateTime datePaid, PaymentTypeModel paymentType)
        {
            ID = id;
            PaymentStatus = paymentStatus;
            Amount = amount;
            DatePaid = datePaid;
            PaymentType = paymentType;
        }

        public PaymentModel() { }

        #endregion
    }
}
