using Exercise3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exercise3.ViewModel
{
    public class ProductViewModel
    {
        public IEnumerable<Manufacturer> Manufacturers { get; set; }
        public Product Product { get; set; }
    }
}