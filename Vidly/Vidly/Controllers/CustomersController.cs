using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
           // var customers = _context.Customers.Include(c => c.MemberShipType).ToList();
            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MemberShipType).SingleOrDefault(e => e.Id == id);
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CustomerFormViewModel vModel)
        {

             if(!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                   Customer = vModel.Customer,
                   MembershipTypes = _context.MembershipTypes.ToList()
                  
                };
                return View("CustomerForm", viewModel);
            }

            if (vModel.Customer.Id == 0)
                _context.Customers.Add(vModel.Customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == vModel.Customer.Id);

                customerInDb.Name = vModel.Customer.Name;
                customerInDb.BirthDate = vModel.Customer.BirthDate;
                customerInDb.IsSubscribedToNewsLetter = vModel.Customer.IsSubscribedToNewsLetter;
                customerInDb.MembershipTypeId = vModel.Customer.MembershipTypeId;

            }

            _context.Customers.Add(vModel.Customer);
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(e => e.Id == id);
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}