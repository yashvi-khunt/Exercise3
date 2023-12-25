using Exercise3.Models;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exercise3.Models
{
    public class Product
    {
        public int Id { get; set; }


        [Required]
        [StringLength(50)]
        [Index("IX_Products", IsUnique = true)]
        public string Name { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int ManufacturerId {  get; set; }

        public bool IsDeleted {  get; set; }

        public ICollection<Rate> Rates { get; set; }
        public ICollection<PurchaseHistory> PurchaseHistorys { get; set; }
    }
}

public static class ProductExtensions
{
    public static void MarkDeleted(this Product product)
    {
        if (product == null) throw new ArgumentNullException(nameof(product));
        product.IsDeleted = true;

        if (product.Rates != null)
        {
            foreach (var rate in product.Rates)
            {
                rate.IsDeleted = true;
            }
        }
    }
}