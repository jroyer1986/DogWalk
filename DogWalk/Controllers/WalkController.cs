using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DogWalk.Data.Repositories;
using DogWalk.Data.Models;
using DogWalk.Helpers;

namespace DogWalk.Controllers
{
    public class WalkController : Controller
    {
        WalkRepository _walkRepository = new WalkRepository();

        // GET: Walk
        public ActionResult Index()
        {


            return RedirectToAction("GetListOfScheduledWalks");
        }

        //[HttpGet]
        //public ActionResult ScheduleWalksByDate()
        //{
        //    WalkModel walkModel = new WalkModel();
        //    return View(walkModel);
        //}
        [HttpGet]
        public ActionResult ScheduleWalksByDate()
        {
            var listOfWalksToSchedule = new ListOfWalksToSchedule();
            ViewBag.DaysOfWeekList = HtmlHelpers.ToSelectListEnumerable(typeof(DayOfWeek),null);

            WalkStatusModel walkStatusModel = new WalkStatusModel(); 
            WalkerModel walkerModel = new WalkerModel();
            PaymentModel paymentModel = new PaymentModel();

            return View(listOfWalksToSchedule);
        }
        
        [HttpPost]
        public ActionResult ScheduleWalksByDate(ListOfWalksToSchedule scheduledWalks, string[] dayOfWeek)
        {

            var validDaysOfWeek = dayOfWeek.Where(m => !m.Equals("false"));
            scheduledWalks.DaysToCheck = new List<DayOfWeek>();
            foreach (var day in validDaysOfWeek)
            {
                DayOfWeek date = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), day);
                scheduledWalks.DaysToCheck.Add(date);
            }
            
            if(!scheduledWalks.EndDate.HasValue)
            {
                scheduledWalks.EndDate  = scheduledWalks.StartDate.Value.AddDays(1);
            }
            _walkRepository.ScheduleWalks(scheduledWalks.StartDate.Value, scheduledWalks.EndDate.Value, scheduledWalks.DaysToCheck);
            return RedirectToAction("GetListOfScheduledWalks");
        }

        public ActionResult GetListOfScheduledWalks()
        {
            var listOfScheduledWalks = _walkRepository.GetWalks();
            return View(listOfScheduledWalks);
        }

        public ActionResult GetWalkByID(int id)
        {
            WalkModel walkModel = _walkRepository.GetWalk(id);
            return View(walkModel);
        }

        public ActionResult CancelWalk(WalkModel walkModel)
        {
            _walkRepository.CancelWalk(walkModel.ID);
            return RedirectToAction("Index");
        }

        public void PayForWalksByDateRange()
        { }

    }
}