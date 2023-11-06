using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Permissions;
using System.Web;

namespace Exercise3.Models
{
    public class PurchaseHistory
    {
        public int Id {  get; set; }
        public int InvoiceId { get; set; }

        public ApplicationUser User { get; set; }

        public Manufacturer Manufacturer { get; set; }
        public int ManufacturerId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }

        public Rate Rate { get; set; }
        public int RateId { get; set; }

        public int Quantity {  get; set; }
        public DateTime Date { get; set; }

        public bool IsDeleted { get; set; }

    }
}