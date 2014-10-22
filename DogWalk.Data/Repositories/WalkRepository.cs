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
        DogWalkDatabaseEntities _dogWalkDatabaseEntities = new DogWalkDatabaseEntities();

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

        public IEnumerable<WalkModel>GetWalks()
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
                WalkStatu walkstatus = walk.WalkStatu;  
                WalkStatusModel walkStatusModel = new WalkStatusModel(walkstatus.ID, walkstatus.Status, walkstatus.Explanation);
                
                Walker walker = walk.Walker;
                WalkerModel walkerModel = new WalkerModel(walker.ID, walker.Name, walker.Phone, walker.Email);

                PaymentType paymentType = walk.Payment.PaymentType;
                PaymentTypeModel paymentTypeModel = new PaymentTypeModel(paymentType.ID, paymentType.PaymentType1, paymentType.Explanation);

                PaymentStatu paymentStatu = walk.Payment.PaymentStatu;
                PaymentStatusModel paymentStatusModel = new PaymentStatusModel(paymentStatu.ID, paymentStatu.Status, paymentStatu.Explanation);

                Payment payment = walk.Payment;
                PaymentModel paymentModel = new PaymentModel(payment.ID, paymentStatusModel, payment.Amount, payment.DatePaid, paymentTypeModel);
                

                //convert walk from database type to model for controller to use
                WalkModel walkModel = new WalkModel(walk.ID, walk.DateOfWalk, walkStatusModel, walkerModel, paymentModel);
                
                walksForController.Add(walkModel);
            }

            return walksForController;
        }

        public WalkModel GetWalks(int id)
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
                WalkStatu walkStatus = walk.WalkStatu;
                WalkStatusModel walkStatusModel = new WalkStatusModel(walkStatus.ID, walkStatus.Status, walkStatus.Explanation);

                Walker walker = walk.Walker;
                WalkerModel walkerModel = new WalkerModel(walker.ID, walker.Name, walker.Phone, walker.Email);

                PaymentType paymentType = walk.Payment.PaymentType;
                PaymentTypeModel paymentTypeModel = new PaymentTypeModel(paymentType.ID, paymentType.PaymentType1, paymentType.Explanation);

                PaymentStatu paymentStatus = walk.Payment.PaymentStatu;
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
    }
}
