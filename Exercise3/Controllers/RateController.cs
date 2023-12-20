using Exercise3.Models;
using Exercise3.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Exercise3.Controllers
{
    public class RateController : Controller
    {
        private ApplicationDbContext _context;

        public RateController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Rate
        public ActionResult Index()
        {   
            var rates = _context.Rates.Include(r => r.Product).Where(r => r.IsDeleted == false).OrderBy(r => r.ProductId).ThenByDescending(r => r.Date).ToList();
            return View(rates);
        }

        public ActionResult Add() 
        {
            var products = _context.Products.ToList();
            var viewModel = new RateFormViewModel
            {
                Products = products,
                Rate = new Rate()
            };
            return View("RateForm",viewModel);
        }

        public ActionResult Edit(int id) 
        {
            var rate = _context.Rates.SingleOrDefault(p => p.Id == id);

            if (rate == null)
                return HttpNotFound();

            var viewModel = new RateFormViewModel
            {
                Rate = rate,
                Products = _context.Products.ToList()
            };

            return View("RateForm", viewModel);
        }
        public ActionResult Delete(int id)
        {
            var rate = _context.Rates.SingleOrDefault(r => r.Id == id);

            if (rate == null)
                return HttpNotFound();

            rate.IsDeleted = true;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index", "Rate");

        }
        [HttpPost]
        public ActionResult Save(Rate rate)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new RateFormViewModel
                {
                    Rate = rate,
                    Products = _context.Products.ToList()
                };
                return View("RateForm", viewModel);
            }

            if(rate.Id == 0)
            {
                _context.Rates.Add(rate);
            }
            else
            {
                var RateInDb = _context.Rates.Single(r => r.Id == rate.Id);
                RateInDb.ProductId = rate.ProductId;
                RateInDb.Amount = rate.Amount;
                RateInDb.Date = rate.Date;  
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index", "Rate");

        }
    }
}