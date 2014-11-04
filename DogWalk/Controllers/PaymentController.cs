using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DogWalk.Data.Repositories;
using DogWalk.Data.Models;

namespace DogWalk.Controllers
{
    public class PaymentController : Controller
    {
        PaymentRepository _paymentRepository = new PaymentRepository();

        // GET: Payment
        public ActionResult Index()
        {
            return RedirectToAction("GetPayments");
        }

        public ActionResult GetPaymentByID(int id)
        {
           var payment = _paymentRepository.GetPaymentByID(id);
           return View(payment);
        }

        public ActionResult GetPayments()
        {
            var listOfPayments = _paymentRepository.GetPayments();
            return View(listOfPayments);
        }

        [HttpGet]
        public ActionResult Schedule()
        {
            var listOfPaymentTypes = _paymentRepository.GetListOfPaymentTypes();

            if(listOfPaymentTypes != null)
            {
                ViewBag.ListOfPaymentTypes = listOfPaymentTypes.Select(m => new SelectListItem()  { Text = m.PaymentType, Value = m.ID.ToString()});
            }
            else
            {
                ViewBag.ListOfPaymentTypes = new List<SelectListItem>()
                {
                    new SelectListItem(){Text = "N/A", Value = "-1"}
                };
            }

            PaymentModel paymentModel = new PaymentModel();
            return View(paymentModel);
        }

        [HttpPost]
        public ActionResult Schedule(PaymentModel payment, DateTime? dateStart, DateTime? dateEnd)
        {
            _paymentRepository.SchedulePayment(payment, dateStart, dateEnd);
            return RedirectToAction("Index");
        }

        public ActionResult Cancel(PaymentModel paymentToCancel)
        {
            _paymentRepository.CancelPayment(paymentToCancel);
            return RedirectToAction("GetPaymentByID", new { id = paymentToCancel.ID });
        }
    }
}