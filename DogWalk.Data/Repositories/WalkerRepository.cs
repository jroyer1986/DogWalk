using DogWalk.Data.Entities;
using DogWalk.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogWalk.Data.Repositories
{
    public class WalkerRepository
    {
        DogWalkEntities _dogWalkDatabaseEntities = new DogWalkEntities();


        public WalkerModel GetWalkerByID(int id)
        {
            //get walker from database based on walker ID
            Walker walker = _dogWalkDatabaseEntities.Walkers.FirstOrDefault(m => m.ID == id);

            if (walker != null)
            {
                //convert walker to WalkerID to pass to controller
                WalkerModel walkerModel = new WalkerModel(walker.ID, walker.Name, walker.Phone, walker.Email);
                return walkerModel;
            }
            else
            { return null; }    
            
        }

        public IEnumerable<WalkerModel> GetWalkers(string name = null, string phone = null, string email = null, bool disabled = false)
        {
            //get list of walkers from database based on paramaters passed
            var listOfWalkers = _dogWalkDatabaseEntities.Walkers.AsQueryable();
            if(name != null)
            {
                listOfWalkers = listOfWalkers.Where(n => n.Name.Contains(name));
            }

            if(phone != null)
            {
                listOfWalkers = listOfWalkers.Where(p => p.Phone.Contains(phone));        
            }

            if(email != null)
            {
                listOfWalkers = listOfWalkers.Where(e => e.Email.Contains(email));
            }
            if(disabled == false)
            {
                listOfWalkers = listOfWalkers.Where(e => e.Disabled.Equals(false));
            }

            //convert each walker in the list to a WalkerModel and add it to a list of WalkerModels to be passed to the controller
            var listOfWalkerModels = new List<WalkerModel>();

            foreach(Walker walker in listOfWalkers.AsEnumerable())
            {
                WalkerModel walkerModel = new WalkerModel(walker.ID, walker.Name, walker.Phone, walker.Email, walker.Disabled);
                listOfWalkerModels.Add(walkerModel);
            }
            return listOfWalkerModels;
        }        

        public int CreateWalker(WalkerModel walker)
        {
            Walker dbwalker = new Walker();
            dbwalker.Name = walker.Name;
            dbwalker.Phone = walker.Phone;
            dbwalker.Email = walker.Email;
            dbwalker.Disabled = walker.Disabled;

            _dogWalkDatabaseEntities.Walkers.Add(dbwalker);
            _dogWalkDatabaseEntities.SaveChanges();

            return dbwalker.ID;
        }

        public void UpdateWalker(WalkerModel walker)
        {
            //get walker from repository based on its ID
            Walker dbwalker = _dogWalkDatabaseEntities.Walkers.FirstOrDefault(m => m.ID == walker.ID);

            if (dbwalker != null)
            {
                //update walker information and save to repository
                dbwalker.Name = walker.Name;
                dbwalker.Phone = walker.Phone;
                dbwalker.Email = walker.Email;

                _dogWalkDatabaseEntities.SaveChanges();
            }
        }

        public void DeleteWalker(int id)
        {
            //find walker in database based on its id and remove from list
            Walker walkerToDelete = _dogWalkDatabaseEntities.Walkers.FirstOrDefault(m => m.ID == id);

            if (walkerToDelete != null)
            {
                walkerToDelete.Disabled = true;
                _dogWalkDatabaseEntities.SaveChanges();
            }
        }
    }
}
