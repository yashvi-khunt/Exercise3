using Exercise3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exercise3.ViewModel
{
    public class RateFormViewModel
    {
        public IEnumerable<Product> Products{get; set;}
        public Rate Rate { get; set;}
    }
}