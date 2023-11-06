using Exercise3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Exercise3.Controllers
{
    public class ManufacturerController : Controller
    {
        private ApplicationDbContext _context;

        public ManufacturerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Manufacturer
        public ViewResult Index()
        {
            var manufacturer = _context.Manufacturers.ToList();

            return View(manufacturer);
        }

        public ActionResult Add()
        {
            return View("ManufacturerForm");
        }

        [HttpPost]
        public ActionResult Save(Manufacturer manufacturer)
        {
            _context.Manufacturers.Add(manufacturer);
            _context.SaveChanges();
            return RedirectToAction("Index", "Manufacturer");
        }
    }
}