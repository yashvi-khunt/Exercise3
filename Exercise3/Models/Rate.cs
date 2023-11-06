using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Exercise3.Models
{
    public class Rate
    {

        public int Id { get; set; }
        public string Amount { get; set; }

        public DateTime Date { get; set; } 
        public Product Product { get; set; }
        public int ProductId {  get; set; }

        public bool IsDeleted {  get; set; }
    }
}