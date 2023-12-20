using Exercise3.Models;
using Exercise3.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Exercise3.Controllers
{
    public class ProductController : Controller
    {

        private ApplicationDbContext _context;

        public ProductController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Product
        public ActionResult Index()
        {
            var products = _context.Products.Include(p => p.Manufacturer).Where(m => m.IsDeleted == false).ToList();
            //var products = _context.Products.Include(p => p.Manufacturer).Where(p => p.IsDeleted == false && p.Manufacturer.IsDeleted == false).ToList();
            return View(products);
        }

        public ActionResult Add()
        {
            var manufacturers = _context.Manufacturers.ToList();
            var viewModel = new ProductFormViewModel
            {
                Product = new Product(),
                Manufacturers = manufacturers
            };
            return View("ProductForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var product = _context.Products.SingleOrDefault(p => p.Id == id);

            if (product == null)
                return HttpNotFound();

            var viewModel = new ProductFormViewModel
            {
                Product = product,
                Manufacturers = _context.Manufacturers.ToList()
            };

            return View("ProductForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(Product product)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new ProductFormViewModel
                {
                    Product = product,
                    Manufacturers = _context.Manufacturers.ToList()
                };
                return View("ProductForm", viewModel);
            }


            if (product.Id == 0)
            {
                _context.Products.Add(product);
            }
            else
            {
                var productInDb = _context.Products.Single(p => p.Id == product.Id);
                productInDb.Name = product.Name;
                productInDb.ManufacturerId = product.ManufacturerId;
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index", "Product");
        }


        public ActionResult Delete(int id)
        {
            var product = _context.Products.Include(p => p.Rates).SingleOrDefault(p => p.Id == id);

            if (product == null)
                return HttpNotFound();

            product.MarkDeleted();
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return RedirectToAction("Index", "Product");
        }
    }
}