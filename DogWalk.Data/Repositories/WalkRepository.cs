using DogWalk.Data.Entities;
using DogWalk.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DogWalk.Data.Repositories
{
    class WalkRepository
    {
        //Create an instance of the repository
        DogWalkEntities _dogWalkDatabaseEntities = new DogWalkEntities();

        private static decimal costPerWalk = 23;

        public int CreateWalk(WalkModel newWalk)
        {
            //convert walkmodel to a walk for the database
            Walk dbWalk = new Walk();             
            dbWalk.DateOfWalk = newWalk.DateOfWalk;
            dbWalk.WalkStatusID = newWalk.Status.ID;
            dbWalk.WalkerID = newWalk.Walker.ID;
            dbWalk.PaymentID = newWalk.Payment.ID;

            _dogWalkDatabaseEntities.Walks.Add(dbWalk);
            _dogWalkDatabaseEntities.SaveChanges();

            return dbWalk.ID;
        }

        public IEnumerable<WalkModel> GetWalks()
        {
            //create a list of walks from the database and save them as a variable
            var walks = _dogWalkDatabaseEntities.Walks
                                        //name of related property in entity
                                        .Include("WalkStatus")
                                        .Include("Payment")
                                        .Include("Walker")
                                        .AsEnumerable();

            List<WalkModel> walksForController = new List<WalkModel>();

            foreach(Walk walk in walks)
            {
                //convert WalkStatus to WalkStatusModel
                WalkStatus walkstatus = walk.WalkStatus;  
                WalkStatusModel walkStatusModel = new WalkStatusModel(walkstatus.ID, walkstatus.Status, walkstatus.Explanation);
                
                Walker walker = walk.Walker;
                WalkerModel walkerModel = new WalkerModel(walker.ID, walker.Name, walker.Phone, walker.Email);

                PaymentType paymentType = walk.Payment.PaymentType;
                PaymentTypeModel paymentTypeModel = new PaymentTypeModel(paymentType.ID, paymentType.PaymentType1, paymentType.Explanation);

                PaymentStatus paymentStatu = walk.Payment.PaymentStatus;
                PaymentStatusModel paymentStatusModel = new PaymentStatusModel(paymentStatu.ID, paymentStatu.Status, paymentStatu.Explanation);

                Payment payment = walk.Payment;
                PaymentModel paymentModel = new PaymentModel(payment.ID, paymentStatusModel, payment.Amount, payment.DatePaid, paymentTypeModel);
                

                //convert walk from database type to model for controller to use
                WalkModel walkModel = new WalkModel(walk.ID, walk.DateOfWalk, walkStatusModel, walkerModel, paymentModel);
                
                walksForController.Add(walkModel);
            }

            return walksForController;
        }

        public WalkModel GetWalk(int id)
        {
            //get walk from the db
            Walk walk = _dogWalkDatabaseEntities.Walks
                                                .Include("Payment")
                                                .Include("Walker")
                                                .Include("WalkStatu")
                                                .FirstOrDefault(m => m.ID == id);
            if (walk != null)
            {
                //load walkstatu, walker and payment models
                WalkStatus walkStatus = walk.WalkStatus;
                WalkStatusModel walkStatusModel = new WalkStatusModel(walkStatus.ID, walkStatus.Status, walkStatus.Explanation);

                Walker walker = walk.Walker;
                WalkerModel walkerModel = new WalkerModel(walker.ID, walker.Name, walker.Phone, walker.Email);

                PaymentType paymentType = walk.Payment.PaymentType;
                PaymentTypeModel paymentTypeModel = new PaymentTypeModel(paymentType.ID, paymentType.PaymentType1, paymentType.Explanation);

                PaymentStatus paymentStatus = walk.Payment.PaymentStatus;
                PaymentStatusModel paymentStatusModel = new PaymentStatusModel(paymentStatus.ID, paymentStatus.Status, paymentStatus.Explanation);

                Payment payment = walk.Payment;
                PaymentModel paymentModel = new PaymentModel(payment.ID, paymentStatusModel, payment.Amount, payment.DatePaid, paymentTypeModel);
                

                //convert walk to WalkModel for the controller to use
                WalkModel walkModel = new WalkModel(walk.ID, walk.DateOfWalk, walkStatusModel, walkerModel, paymentModel);

                return walkModel;   
            }
            else
            {
                return null;
            }
        }

        public void UpdateWalkStatus(WalkModel walk)
        {
            //get walk from the repository based on its ID
            Walk walkToUpdate = _dogWalkDatabaseEntities.Walks.FirstOrDefault(m => m.ID == walk.ID);

            if (walkToUpdate != null)
            {
                walkToUpdate.WalkStatusID = walk.Status.ID;              
                _dogWalkDatabaseEntities.SaveChanges();
            }

        }

        public List<DateTime> ScheduleWalks(DateTime startDate, DateTime endDate, List<DayOfWeek>daysToCheck, WalkModel walk)
        {
            var listOfWalkDates = new List<DateTime>();

            if (startDate >= endDate)
                return listOfWalkDates;

            if (daysToCheck == null || daysToCheck.Count == 0)
                return listOfWalkDates;

            try
            {
                //get the total number of days between the 2 given dates
                var totalDays = (int)endDate.Subtract(startDate).TotalDays;
                               
                //create a list of the dates between the given dates
                var allDatesQry = from d in Enumerable.Range(1, totalDays)
                                  select new DateTime(startDate.AddDays(d).Year,
                                  startDate.AddDays(d).Month,
                                  startDate.AddDays(d).Day);                

                //extract the days of the week we specifically requested
                var selectedDatesQry = from d in allDatesQry
                                       where daysToCheck.Contains(d.DayOfWeek)
                                       select d;

                //add those selected dates to a new list of dates to schedule
                listOfWalkDates = selectedDatesQry.ToList();

                //create a new Walk for each walkdate in listOfWalkDates
                if(listOfWalkDates != null)
                {
                    foreach(DateTime walkDate in listOfWalkDates)
                    {
                        walk.DateOfWalk = walkDate;
                        CreateWalk(walk);
                    }                  
                       
                }
            }
            
            catch (Exception ex)
            {
                throw;
            }
                return listOfWalkDates;
        }

        public void PayWalks(IEnumerable<WalkModel> walks)
        {            
            foreach (WalkModel walk in walks)
            {
                Walk walkToPay = _dogWalkDatabaseEntities.Walks.FirstOrDefault(m => m.ID == walk.ID);

                if (walkToPay != null)
                {
                    walkToPay.PaymentID = walk.Payment.ID;

                    _dogWalkDatabaseEntities.SaveChanges();
                }

            }


        }
    }
}
