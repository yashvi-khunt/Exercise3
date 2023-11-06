using Exercise3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

            var products = _context.Products.ToList();
            return View(products);
        }
    }
}