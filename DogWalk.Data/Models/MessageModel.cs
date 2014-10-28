using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogWalk.Data.Models
{
    public class MessageModel
    {
        #region Properties

        public int ID { get; set; }
        public string Message { get; set; }
        public DateTime DateOfMessage { get; set; } 

        #endregion

        #region Constructor

        public MessageModel() { }

        public MessageModel(int id, string message, DateTime dateOfMessage)
        {
            ID = id;
            Message = message;
            DateOfMessage = dateOfMessage;
        }

        #endregion
    }
}
