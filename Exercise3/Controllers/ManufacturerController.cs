using Exercise3.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace Exercise3.Controllers
{
    //[Authorize(Roles = RoleNames.CanManageCustomers)]
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
            var manufacturer = new Manufacturer();
            return View("ManufacturerForm", manufacturer);
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
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

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
            var manufacturer = _context.Manufacturers.Include(m => m.Products).SingleOrDefault(m => m.Id == id);

            if (manufacturer == null)
                return HttpNotFound();

            foreach (var product in manufacturer.Products)
            {
                product.Rates = _context.Rates.Where(r => r.ProductId == product.Id).ToList();
            }

            manufacturer.MarkDeleted();
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(ex); }
            return RedirectToAction("Index", "Manufacturer");
        }
    }
}