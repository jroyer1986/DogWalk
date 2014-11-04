using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DogWalk.Data.Repositories;
using DogWalk.Data.Models;

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
        
        public ActionResult ScheduleWalksByDate(DateTime DateRangeStart, DateTime? DateRangeEnd, DayOfWeek[] DayOfWeek, WalkModel Walk)
        {
            if(DateRangeEnd == null)
            {
                DateRangeEnd = DateRangeStart.AddDays(1);
            }
            _walkRepository.ScheduleWalks(DateRangeStart, DateRangeEnd.Value, DayOfWeek.ToList(), Walk);
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
            return RedirectToAction("GetWalkByID", new { id = walkModel.ID });
        }

        public void PayForWalksByDateRange()
        { }

    }
}