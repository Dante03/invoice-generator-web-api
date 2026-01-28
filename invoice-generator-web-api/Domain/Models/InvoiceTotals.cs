namespace invoice_generator_web_api.Domain.Models
{
    public class InvoiceTotals
    {
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
    }
}
