using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogWalk.Data.Models;

namespace DogWalk.Data.Repositories
{
    class PaymentRepository
    {
        //Create an instance of the repository
        DogWalkDatabaseEntities _dogWalkDatabaseEntities = new DogWalkDatabaseEntities();

        public void SchedulePayment(DateTime dateStart, DateTime dateEnd, PaymentModel payment)
        {
            //find any walks that were scheduled for those days that werent already paid.  
            //Add them to a list and create a paymentModel for them

            var listOfDaysToPay = _dogWalkDatabaseEntities.Walks.Where(d => d.DateOfWalk >= dateStart && d.DateOfWalk <= dateEnd)
                                                                .Where(p => p.Payment.PaymentStatus.Status != "paid");

            Payment newPayment = new Payment()
            {
                
            }
            


            foreach(Walk walk in listOfDaysToPay)
                if(walk != null)
                {
                    walk.PaymentID = payment.ID;
                    _dogWalkDatabaseEntities.SaveChanges();
                }



        }

        public void CancelPayment(PaymentModel payment)
        {
            //get payment by ID
            Payment paymentToCancel = _dogWalkDatabaseEntities.Payments.FirstOrDefault(m => m.ID == payment.ID);

            //update payment status to cancelled
            if(paymentToCancel != null)
            {
                var cancelStatus = _dogWalkDatabaseEntities.PaymentStatus.FirstOrDefault(m => m.Status == "cancelled");

                paymentToCancel.PaymentStatusID = cancelStatus.ID;
                _dogWalkDatabaseEntities.SaveChanges();
            }
            
        }

        public void SendPayment() { }

        public PaymentModel GetPayment(int id)
        {
            //get payment from the database
            Payment payment = _dogWalkDatabaseEntities.Payments
                                                      .Include("PaymentType")
                                                      .Include("PaymentStatus")
                                                      .FirstOrDefault(m => m.ID == id);

            if(payment != null)
            {
                //convert paymentStatus and paymentType
                PaymentStatus paymentStatus = payment.PaymentStatus;
                PaymentStatusModel paymentStatusModel = new PaymentStatusModel(paymentStatus.ID, paymentStatus.Status, paymentStatus.Explanation);

                PaymentType paymentType = payment.PaymentType;
                PaymentTypeModel paymentTypeModel = new PaymentTypeModel(paymentType.ID, paymentType.PaymentType1, paymentType.Explanation);

                //convert payment to paymentModel for controller to use
                PaymentModel paymentModel = new PaymentModel(payment.ID, paymentStatusModel, payment.Amount, payment.DatePaid, paymentTypeModel);
                return paymentModel;
            }
            else
            { return null; }
        }

        public void SearchPayments() { }

    }
}
