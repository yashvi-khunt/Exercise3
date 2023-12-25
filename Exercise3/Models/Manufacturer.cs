using Exercise3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Exercise3.Models
{
    public class Manufacturer
    {

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Index("IX_Manufacturers", IsUnique = true)]
        public string Name { get; set; }

        public bool IsDeleted {  get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<PurchaseHistory> PurchaseHistorys { get; set;}
    }
}

public static class ManufacturerExtensions
{
    public static void MarkDeleted(this Manufacturer manufacturer)
    {
        if (manufacturer == null) throw new ArgumentNullException(nameof(manufacturer));
        manufacturer.IsDeleted = true;

        if(manufacturer.Products != null)
        {
            foreach (var product in manufacturer.Products)
            {
                product.IsDeleted = true;
                if(product.Rates != null)
                {
                    foreach (var rate in product.Rates)
                    {
                        rate.IsDeleted = true;
                    }
                }
            } 
        }
    }
}