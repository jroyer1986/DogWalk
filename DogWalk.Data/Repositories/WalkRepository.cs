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

        public IEnumerable<WalkModel>GetWalk()
        {
            //create a list of walks from the database and save them as a variable
            var walks = _dogWalkDatabaseEntities.Walks
                                        //name of related property in entity
                                        .Include("WalkStatus")
                                        .AsEnumerable();

            List<WalkModel> walksForController = new List<WalkModel>();

            foreach(Walk walk in walks)
            {
                //convert WalkStatus to WalkStatusModel
                WalkStatus walkstatus = walk.WalkStatus;
                //convert it here
                WalkStatusModel walkStatusModel = new WalkStatusModel(walkstatus.ID, walkstatus.Status, walkstatus.Explanation);
                
                Walker walker = walk.Walker;
                WalkerModel walkerModel = new WalkerModel(walker.ID, walker.Name, walker.Phone, walker.Email);

                Payment payment = walk.
                PaymentModel paymentModel = new PaymentModel(payment.ID, payment.PaymentStatu, payment.Amount, payment.DatePaid, payment.PaymentType);

                //convert walk from database type to model for controller to use
                WalkModel walkModel = new WalkModel(walk.ID, walk.DateOfWalk, walkStatusModel, walkerModel, paymentModel);
                
                walksForController.Add(walkModel);
            }

            return walksForController;
        }
    }
}
