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
    }
}

