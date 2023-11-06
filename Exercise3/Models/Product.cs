using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Exercise3.Models
{
    public class Product
    {
        public int Id { get; set; }


        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public int ManufacturerId {  get; set; }

        public bool IsDeleted {  get; set; }
    }
}