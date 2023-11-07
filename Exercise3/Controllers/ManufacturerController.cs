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
            var manufacturer = _context.Manufacturers.Where(m => m.IsDeleted == false).ToList();

            return View(manufacturer);
        }

        public ActionResult Add()
        {
            return View("ManufacturerForm");
        }

        [HttpPost]
        public ActionResult Save(Manufacturer manufacturer)
        {
            if (!ModelState.IsValid)
            {
                return View("ManufacturerForm", manufacturer);
            }

            if (manufacturer.Id == 0)
            {
                _context.Manufacturers.Add(manufacturer);
            }
            else
            {
                var manufacturerInDb = _context.Manufacturers.Single(m => m.Id == manufacturer.Id);
                manufacturerInDb.Name = manufacturer.Name;
            }
            _context.SaveChanges();
            return RedirectToAction("Index", "Manufacturer");
        }

        public ActionResult Edit(int id)
        {
            var manufacturer = _context.Manufacturers.SingleOrDefault(m => m.Id == id);

            if (manufacturer == null)
                return HttpNotFound();

            return View("ManufacturerForm", manufacturer);

        }

        public ActionResult Delete(int id)
        {
            var manufacturer = _context.Manufacturers.SingleOrDefault(m => m.Id == id);

            if (manufacturer == null)
                return HttpNotFound();

            manufacturer.IsDeleted = true;
            _context.SaveChanges();
            return RedirectToAction("Index", "Manufacturer");
        }
    }
}