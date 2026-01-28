using invoice_generator_web_api.Domain.Models;
using invoice_generator_web_api.Domain.Pdf;
using QuestPDF.Fluent;


namespace invoice_generator_web_api.Services
{
    public interface IInvoicePdfService
    {
        byte[] Generate(Invoice invoice);
    }

    public class InvoicePdfService : IInvoicePdfService
    {
        public byte[] Generate(Invoice invoice)
        {
            var document = new InvoicePdf(invoice);
            return document.GeneratePdf();
        }
    }
}
