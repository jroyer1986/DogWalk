using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DogWalk.Data.Repositories;
using DogWalk.Data.Models;

namespace DogWalk.Controllers
{
    public class WalkerController : Controller
    {
        WalkerRepository _walkerRepository = new WalkerRepository();

        // GET: Walker
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            WalkerModel walkerModel = new WalkerModel();

            return View(walkerModel);
        }

        [HttpPost]
        public ActionResult Create(WalkerModel newWalker)
        {
            _walkerRepository.CreateWalker(newWalker);
            return RedirectToAction("Index");
        }

        public ActionResult ViewWalkerList()
        {
            IEnumerable<WalkerModel> walkerList = _walkerRepository.GetWalkers();
            return View(walkerList);
        }

        public ActionResult ViewWalker(int id)
        {
            _walkerRepository.GetWalkerByID(id);
            return View();
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            WalkerModel walkerModel = _walkerRepository.GetWalkerByID(id);
            if(walkerModel != null)
            {
                return View(walkerModel);
            }
            else
            {
                return View("Error");
            }
        }

        [HttpPost]  
        public ActionResult Update(WalkerModel updatedWalker)
        {
            _walkerRepository.UpdateWalker(updatedWalker);
            return RedirectToAction("ViewWalker", new { id = updatedWalker.ID });
        }
    }
}