using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogWalk.Data.Models
{
    public class ListOfWalksToSchedule
    {
        public DateTime? StartDate {get;set;}
        public DateTime? EndDate {get;set;}
        public List<DayOfWeek> DaysToCheck {get;set;}

        public ListOfWalksToSchedule(DateTime startDate, DateTime? endDate, List<DayOfWeek> daysToCheck, WalkModel walkModel)
        {
            StartDate = startDate;
            EndDate = endDate.Value;
            DaysToCheck = daysToCheck;
            
        }

        public ListOfWalksToSchedule() { }

    }
}
