using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exercise3.Models
{
    public class Manufacturer
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool IsDeleted {  get; set; }
    }
}