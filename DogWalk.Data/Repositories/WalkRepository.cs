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
                                        .Include("WalkStatu")
                                        .AsEnumerable();

            List<WalkModel> walksForController = new List<WalkModel>();

            foreach(Walk walk in walks)
            {
                //WALKSTATUSMODEL
                WalkStatu walkstatu = walk.WalkStatu;
                //convert it here
                WalkStatusModel walkStatusModel = new WalkStatusModel(){
                        ID = walkstatu.ID,
                         Explanation = walkstatu.Explanation,
                          Status = walkstatu.Status
                };
                //WALKERMODEL
                Walker walker = walk.Walker;
                WalkerModel walkerModel = new WalkerModel(){
                    ID = walker.ID,
                    Name = walker.Name,
                    Phone = walker.Phone,
                    Email = walker.Email
                };
           
                //Payment Model
                Payment payment = null; //should equal walk.Payment, but you havent mapped this in repo

                //PaymentModel
                PaymentModel paymentModel = null; //should be a new paymentModel

                //convert walk from database type to model for controller to use
                WalkModel walkModel = new WalkModel(walk.ID, walk.DateOfWalk, walkStatusModel, walkerModel, paymentModel);
            }
        }
    }
}
