using DogWalk.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogWalk.Data.Repositories
{
    class MessageRepository
    {
        DogWalkDatabaseEntities _dogWalkDatabaseEntities = new DogWalkDatabaseEntities();

        public void SendMessage() { }

        public void GetMessages()
        {
            var listOfMessages = _dogWalkDatabaseEntities.Messages.ToList();

            if(listOfMessages != null)
            {
                foreach(Message message in listOfMessages)
                {
                    MessageModel messageModel = new MessageModel()
                }
            }
        }

        public void GetMessageByID() { }    
    }
}
