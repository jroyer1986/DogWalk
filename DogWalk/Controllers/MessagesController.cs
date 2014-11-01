using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DogWalk.Data.Repositories;
using DogWalk.Data.Models;

namespace DogWalk.Controllers
{
    public class MessagesController : Controller
    {
        MessageRepository _messageRepository = new MessageRepository();
        WalkerRepository _walkerRepository = new WalkerRepository();

        // GET: Messages
        public ActionResult Index()
        {
            return RedirectToAction("ViewListOfMessages");
        }

        [HttpGet]
        public ActionResult Send()
        {
            var listOfWalkers = _walkerRepository.GetWalkers();

            if(listOfWalkers != null)
            {
                ViewBag.ListOfWalkers = listOfWalkers.Select(m => new SelectListItem() { Text = m.Name, Value = m.ID.ToString() });
            }
            else
            {
                ViewBag.ListOfWalkers = new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "No Walkers", Value = "-1"}
                };
            }
            MessageModel messageModel = new MessageModel();

            return View(messageModel);
        }
        
        [HttpPost]
        public ActionResult Send(MessageModel message)
        {
            _messageRepository.SendMessage(message);

            return RedirectToAction("Index");
        }

        public ActionResult ViewMessage(MessageModel message)
        {
            MessageModel messageToView = _messageRepository.GetMessageByID(message.ID);

            return View(messageToView);
        }

        public ActionResult ViewListOfMessages(DateTime? dateStart = null, DateTime? dateEnd = null, int? id = null)
        {
            var listOfMessages = _messageRepository.GetMessages(dateStart, dateEnd, id);

            var listOfWalkers = _walkerRepository.GetWalkers();

            if (listOfWalkers != null)
            {
                ViewBag.ListOfWalkers = listOfWalkers.Select(m => new SelectListItem() { Text = m.Name, Value = m.ID.ToString() });
            }
            else
            {
                ViewBag.ListOfWalkers = new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "No Walkers", Value = "-1"}
                };
            }

            return View(listOfMessages);
        }

    }
}