using invoice_generator_web_api.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace invoice_generator_web_api.DTOs
{
    public class CreateInvoiceRequest
    {
        public Company Seller { get; set; }
        public Company Client { get; set; }
        public List<InvoiceItem> Items { get; set; }

        public decimal taxRate { get; set; }
        public decimal Discount { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
    }
}
