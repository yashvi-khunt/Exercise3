using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        [ForeignKey("AspNetUserId")]
        public ApplicationUser User { get; set; }
        [Display(Name = "User Name")]
        public string AspNetUserId { get; set; }

        public Manufacturer Manufacturer { get; set; }
        [Display(Name = "Manufacturer")]
        public int ManufacturerId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }

        public Rate Rate { get; set; }
        public int RateId { get; set; }
        [Required]
        public int Quantity {  get; set; }
        [Required]
        public DateTime? Date { get; set; }

        public bool IsDeleted { get; set; }

    }
}