using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DogWalk.Data.Entities;

namespace DogWalk.Data.Models
{
    public class PaymentTypeModel
    {
        #region Properties

        public int ID { get; set; }
        public string PaymentType { get; set; }
        public string Explanation { get; set; }

        #endregion

        #region Constructor

        public PaymentTypeModel(int id, string paymentType, string explanation)
        {
            ID = id;
            PaymentType = paymentType;
            Explanation = explanation;
        }

        public PaymentTypeModel() { }

        public PaymentTypeModel(PaymentType paymentType)
        {
            ID = paymentType.ID;
            PaymentType = paymentType.PaymentType1;
            Explanation = paymentType.Explanation;
        }

        #endregion
    }
}
