using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Exercise3.Models
{
    public class Rate
    {

        public int Id { get; set; }
        [Display(Name = "Rate Amount")]
        [Required]
        public float? Amount { get; set; }
        [Required]
        public DateTime? Date { get; set; } 
        public Product Product { get; set; }
        [Display(Name = "Product")]
        public int ProductId {  get; set; }

        public bool IsDeleted {  get; set; }
        public ICollection<PurchaseHistory> PurchaseHistorys { get; set; }
    }
}