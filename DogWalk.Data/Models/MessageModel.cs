using DogWalk.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogWalk.Data.Entities;
using DogWalk.Data.Models.Enums;

namespace DogWalk.Data.Models
{
    public class MessageModel
    {
        #region Properties

        public int ID { get; set; }
        public WalkerModel Walker { get; set; }
        public string Body { get; set; }
        public DateTime DateSent { get; set; }
        public Enums.ContactMethod ContactMethod { get; set; }

        #endregion

        #region Constructor

        public MessageModel()
        {
            DateSent = DateTime.Now;
            Walker = new WalkerModel();
        }

        public MessageModel(int id, WalkerModel walker, string body, DateTime dateSent, Enums.ContactMethod contactMethod)
        {
            ID = id;
            Walker = walker;
            Body = body;
            DateSent = dateSent;
            ContactMethod = ContactMethod;
        }

        public MessageModel(Message message)
        {
            Walker walker = message.Walker;
            WalkerModel walkerModel = new WalkerModel(walker.ID, walker.Name, walker.Phone, walker.Email);
            
            ID = message.ID;
            Walker = walkerModel;
            Body = message.Body;
            DateSent = message.DateSent;
            ContactMethod = (Enums.ContactMethod) message.ContactMethod.ID;
        }

        #endregion
    }
}
