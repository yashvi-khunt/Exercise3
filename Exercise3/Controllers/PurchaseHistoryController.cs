using Exercise3.Models;
using Exercise3.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Exercise3.Controllers
{
    public class PurchaseHistoryController : Controller
    {
        private ApplicationDbContext _context;
        public PurchaseHistoryController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: PurchaseHistory
        public ActionResult Index()
        {
            var history = _context.purchaseHistories.ToList();
            return View(history);
        }

        public ActionResult Add()
        {
            var viewModel = new InvoiceFormViewModel
            {
                Invoice = new PurchaseHistory(),
                Manufacturers = _context.Manufacturers.ToList(),
                Products = _context.Products.ToList(),
                Users = _context.Users.ToList(),
            };
            return View("InvoiceForm", viewModel);
        }

        public ActionResult Save(PurchaseHistory history)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new InvoiceFormViewModel
                {
                    Invoice = history,
                    Manufacturers = _context.Manufacturers.ToList(),
                    Users = _context.Users.ToList(),
                    Products = _context.Products.ToList(),
                };

                return View("InvoiceForm", viewModel);
            }

            if(history.Id == 0)
            {
                _context.purchaseHistories.Add(history);
            }
            return RedirectToAction("Index", "PurchaseHistory");
        }
    }
}