using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace OrderImport {
    public class Customer {
        public int Id { get; set; }
        
        //[Index(IsUnique = true)]
        [MaxLength(100)]
        public string Name { get; set; } = "";

        [Column(TypeName = "decimal(8, 2)")]
        public decimal CreditLimit { get; set; }

        public List<Order> Orders { get; set; } = new();
    }
}
