namespace invoice_generator_web_api.Domain.Models
{
    public class Totals
    {
        public decimal Subtotal { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
    }

}
