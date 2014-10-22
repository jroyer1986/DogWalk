using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogWalk.Data.Models
{
    public class WalkStatusModel
    {
        #region Properties

        public int ID { get; set; }
        public string Status { get; set; }
        public string Explanation { get; set; }

        #endregion

        #region Constructor

        public WalkStatusModel(int id, string status, string explanation)
        {
            ID = id;
            Status = status;
            Explanation = explanation;
        }

        public WalkStatusModel() { }

        #endregion
    }
}
