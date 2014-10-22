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
            var walks = _dogWalkDatabaseEntities.Walks.AsEnumerable();

            List<WalkModel> walksForController = new List<WalkModel>();

            foreach(Walk walk in walks)
            {
                //convert WalkStatusID from int to WalkStatusModel


                //convert walk from database type to model for controller to use
                WalkModel walkModel = new WalkModel(walk.ID, walk.DateOfWalk, walk.WalkStatus.ID, walk.Walker.ID, walk.PaymentID, walk.
            }
        }
    }
}
