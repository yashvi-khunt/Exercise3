using Exercise3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exercise3.ViewModel
{
    public class InvoiceFormViewModel
    {
        public PurchaseHistory Invoice { get; set; }
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}