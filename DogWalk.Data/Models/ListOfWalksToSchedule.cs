using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogWalk.Data.Models
{
    class ListOfWalksToSchedule
    {
        public DateTime StartDate {get;set;}
        public DateTime EndDate {get;set;}
        public DayOfWeek DaysToCheck {get;set;}
        public WalkModel Walk {get;set;}

        public ListOfWalksToSchedule(DateTime startDate, DateTime endDate, DayOfWeek daysToCheck, WalkModel walkModel)
        {
            StartDate = startDate;
            EndDate = endDate;
            DaysToCheck = daysToCheck;
            Walk = walkModel;
        }

        public ListOfWalksToSchedule() { }

    }
}
