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

        public void SendMessage(MessageModel message)
        {
            //create message and add to database
            Message dbmessage = new Message();            
            dbmessage.WalkerID = message.Walker.ID;
            dbmessage.Body = message.Body;
            dbmessage.DateSent = message.DateSent;
            dbmessage.ContactMethodID = message.ContactMethod.ID;

            _dogWalkDatabaseEntities.Messages.Add(dbmessage);
            _dogWalkDatabaseEntities.SaveChanges();
        }

        public IEnumerable<MessageModel> GetMessages(DateTime? dateStart, DateTime? dateEnd, int? id)
        {
            var listOfMessages = _dogWalkDatabaseEntities.Messages.AsQueryable();

            if(dateStart.HasValue && dateEnd.HasValue)
            {
                listOfMessages = listOfMessages.Where(m => m.DateSent >= dateStart.Value && m.DateSent <= dateEnd.Value);
            }                            
            
            if(id != null)
            {
                listOfMessages = listOfMessages.Where(m => m.WalkerID == id);
            } 

            if(listOfMessages != null)
            {
                var listOfMessageModels = new List<MessageModel>();

                foreach(Message message in listOfMessages)
                {
                    MessageModel messageModel = new MessageModel(message);
                    listOfMessageModels.Add(messageModel);      
                }

                return listOfMessageModels;

            }

            else
            {
                return null;
            }
        }

        public MessageModel GetMessageByID(int id)
        {
            //get message from database by its id, and convert it to a message model to return to the controller
            Message dbMessage = _dogWalkDatabaseEntities.Messages.FirstOrDefault(m => m.ID == id);

            MessageModel messageModel = new MessageModel(dbMessage);

            return messageModel;
        }
    }
}
