using System.Reflection.PortableExecutable;

namespace invoice_generator_web_api.Domain.Models
{
    public class Invoice
    {
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }

        public Company Seller { get; set; }
        public Company Client { get; set; }

        public List<InvoiceItem> Items { get; set; } = [];
        public InvoiceTotals Totals { get; set; }

        public string Notes { get; set; }
    }

}
