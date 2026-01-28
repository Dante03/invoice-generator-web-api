using invoice_generator_web_api.Domain.Models;
using invoice_generator_web_api.DTOs;

namespace invoice_generator_web_api.Services
{
    public class InvoiceService
    {
        private readonly IInvoicePdfService _pdf;
        public InvoiceService(IInvoicePdfService pdf)
        {
            _pdf = pdf;
        }

        public byte[] Create(CreateInvoiceRequest request)
        {
            var totals = InvoiceCalculator.Calculate(
            request.Items,
            request.taxRate,
            request.Discount,
            request.Type
        );

            var invoice = new Invoice
            {
                InvoiceNumber = Guid.NewGuid().ToString()[..6],
                InvoiceDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(15),
                Seller = request.Seller,
                Client = request.Client,
                Items = request.Items,
                Totals = totals,
                Notes = request.Notes
            };

            return _pdf.Generate(invoice);
        }
    }

}
