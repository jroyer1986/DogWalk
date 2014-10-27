using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogWalk.Data.Repositories;

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

        public PaymentModel(Payment payment)
        {
            //convert paymentStatus and paymentType
                PaymentStatus paymentStatus = payment.PaymentStatus;
                PaymentStatusModel paymentStatusModel = new PaymentStatusModel(paymentStatus.ID, paymentStatus.Status, paymentStatus.Explanation);

                PaymentType paymentType = payment.PaymentType;
                PaymentTypeModel paymentTypeModel = new PaymentTypeModel(paymentType.ID, paymentType.PaymentType1, paymentType.Explanation);

                //convert payment to paymentModel for controller to use              
                ID = payment.ID;
                PaymentStatus = paymentStatusModel;
                Amount = payment.Amount;
                DatePaid = payment.DatePaid;
                PaymentType = paymentTypeModel;
        }

        #endregion
    }
}
