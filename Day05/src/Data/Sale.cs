using System;
using System.ComponentModel.DataAnnotations;

namespace Day05.Data
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }
        public string InvoiceCode { get; set; }
        public string CustomerName { get; set; }
        public double Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public string OwnerId { get; set; }
    }
}