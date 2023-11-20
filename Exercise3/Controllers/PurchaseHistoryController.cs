using Exercise3.Models;
using Exercise3.ViewModel;
using Newtonsoft.Json;
using System;
using System.Data.Entity;
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
            var purchaseHistories = _context.purchaseHistories.Where(ph=>ph.IsDeleted == false)
       .GroupBy(ph => new { ph.InvoiceId, ph.User.UserName, ph.Date })
       .Select(group => group.FirstOrDefault())
       .Include(ph => ph.User);

            return View(purchaseHistories.ToList());
            
        }

        public ActionResult Details(int id)
        {
            var ph = _context.purchaseHistories.Where(p=>p.InvoiceId == id).Include(m=>m.User).Include(m=>m.Manufacturer).Include(m=>m.Product).Include(m=>m.Rate).ToList();
            return View("Details",ph);
        }

        public ActionResult Delete(int id)
        {
            var ph = _context.purchaseHistories.Where(p => p.InvoiceId == id).ToList();
            if(ph == null)
               return HttpNotFound();

            foreach(var p in ph)
                p.IsDeleted = true;
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index");
            
        }

        public ActionResult Add()
        {
            var viewModel = new InvoiceFormViewModel
            {
                Invoice = new PurchaseHistory(),
                Manufacturers = _context.Manufacturers.Where(m => m.IsDeleted == false).ToList(),
                Products = _context.Products.ToList(),
                Users = _context.Users.ToList(),
            };
            return View("InvoiceForm", viewModel);
        }



        [HttpPost]
        public JsonResult GetProducts(int selectedValue)
        {

            var products = _context.Products.Where(p => p.ManufacturerId == selectedValue && p.IsDeleted == false).ToList();
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetRate(int selectedValue)
        {

            var rate = (from r in _context.Rates
                        where r.ProductId == selectedValue
                        select new { r.Amount, r.Id }).Take(1).ToList();
            return Json(rate);
        }

        public ActionResult Save(InvoiceFormViewModel viewModel)
        {
            List<TableRowData> tableData = JsonConvert.DeserializeObject<List<TableRowData>>(viewModel.TableData);

            foreach (var row in tableData)
            {
                PurchaseHistory ph = new PurchaseHistory
                {
                    InvoiceId = row.InvoiceId,
                    AspNetUserId = row.UserId,
                    ManufacturerId = row.ManufacturerId,
                    ProductId = row.ProductId,
                    RateId = row.RateId,
                    Quantity = row.Quantity,
                    Date = DateTime.Parse(row.Date),
                };

                _context.purchaseHistories.Add(ph);
            }

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return RedirectToAction("Index");
        }
    }

    public class TableRowData
    {
        public int InvoiceId { get; set; }
        public string UserId { get; set; }
        public int ManufacturerId { get; set; }
        public int ProductId { get; set; }
        public int RateId { get; set; }
        public string Date { get; set; }
        public int Quantity { get; set; }
    }
}