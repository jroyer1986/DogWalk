using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogWalk.Data.Models;
using DogWalk.Data.Entities;

namespace DogWalk.Data.Repositories
{
    public class PaymentRepository
    {
        //Create an instance of the repository
        DogWalkEntities _dogWalkDatabaseEntities = new DogWalkEntities();

        public void SchedulePayment(PaymentModel payment, DateTime? dateStart = null, DateTime? dateEnd = null)
        {
            //find any walks that were scheduled for those days that werent already paid.  
            //Add them to a list and create a paymentModel for them

            var listOfDaysToPay = _dogWalkDatabaseEntities.Walks.Where(d => d.DateOfWalk >= dateStart && d.DateOfWalk <= dateEnd)
                                                                .Where(p => p.Payment.PaymentStatus.Status != "Unpaid");

            Payment newPayment = new Payment();

            if(payment.DatePaid >= DateTime.Today)
            {
                newPayment.PaymentStatusID = 1;
            }
            if(payment.DatePaid < DateTime.Today)
            {
                newPayment.PaymentStatusID = 2;
            }
            else
            {
                newPayment.PaymentStatusID = 0;
            }            
            
            newPayment.Amount = payment.Amount;
            newPayment.DatePaid = payment.DatePaid;
            newPayment.PaymentTypeID = payment.PaymentType.ID;
            _dogWalkDatabaseEntities.Payments.Add(newPayment);
            _dogWalkDatabaseEntities.SaveChanges();

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
                var cancelStatus = _dogWalkDatabaseEntities.PaymentStatus1.FirstOrDefault(m => m.Status == "cancelled");

                paymentToCancel.PaymentStatusID = cancelStatus.ID;
                _dogWalkDatabaseEntities.SaveChanges();
            }
            
        }

        public void SendPayment(PaymentModel payment)
        {
            //mark walk as paid.  Send money if paid electronically

            //get payment by ID
            Payment dbPayment = _dogWalkDatabaseEntities.Payments.FirstOrDefault(m => m.ID == payment.ID);
                        
            //change payment status to paid
            if(dbPayment != null)
            {
                var payPayment = _dogWalkDatabaseEntities.PaymentStatus1.FirstOrDefault(m => m.Status == "Paid");

                dbPayment.PaymentStatusID = payPayment.ID;
                _dogWalkDatabaseEntities.SaveChanges();
            }
            
            //send electronic money
        }

        public PaymentModel GetPaymentByID(int id)
        {
            //get payment from the database
            Payment payment = _dogWalkDatabaseEntities.Payments
                                                      .Include("PaymentType")
                                                      .Include("PaymentStatus")
                                                      .FirstOrDefault(m => m.ID == id);

            if(payment != null)
            {
                ////convert paymentStatus and paymentType
                //PaymentStatus paymentStatus = payment.PaymentStatus;
                //PaymentStatusModel paymentStatusModel = new PaymentStatusModel(paymentStatus.ID, paymentStatus.Status, paymentStatus.Explanation);

                //PaymentType paymentType = payment.PaymentType;
                //PaymentTypeModel paymentTypeModel = new PaymentTypeModel(paymentType.ID, paymentType.PaymentType1, paymentType.Explanation);

                //convert payment to paymentModel for controller to use
                PaymentModel paymentModel = new PaymentModel(payment);
                return paymentModel;
            }
            else
            { return null; }
        }

        public IEnumerable<PaymentModel> GetPayments(string paymentType = null, string paymentStatus = null, DateTime? dateStart = null, DateTime? dateEnd = null)
        {
            var paymentList = _dogWalkDatabaseEntities.Payments.AsQueryable();
            if(paymentType != null)
            {
                paymentList = paymentList.Where(p => p.PaymentType.PaymentType1.Contains(paymentType));
            }
            if(paymentStatus !=null)
            {
                paymentList = paymentList.Where(p => p.PaymentStatus.Status.Contains(paymentStatus));
            }
            if(dateEnd >= dateStart && dateEnd != null && dateEnd != null)
            {
                paymentList = paymentList.Where(d => d.DatePaid >= dateStart && d.DatePaid <= dateEnd);
            }
            if(paymentList == null)
            {
                return null;
            }
            return paymentList.AsEnumerable().Select(m => new PaymentModel(m));
            
        }
        public IEnumerable<PaymentTypeModel> GetListOfPaymentTypes()
        {
            var listOfPaymentTypes = _dogWalkDatabaseEntities.PaymentTypes.AsEnumerable().Select(m => new PaymentTypeModel(m));
            return listOfPaymentTypes;
        }

    }
}
