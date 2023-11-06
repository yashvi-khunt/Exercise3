using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exercise3.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int ManufacturerId {  get; set; }

        public bool IsDeleted {  get; set; }
    }
}