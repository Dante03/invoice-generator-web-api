using invoice_generator_web_api.Domain.Models;
using invoice_generator_web_api.DTOs;
using invoice_generator_web_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace invoice_generator_web_api.Controllers
{
    [ApiController]
    [Route("api/invoices")]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceService _service;

        public InvoiceController(InvoiceService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateInvoiceRequest request)
        {
            var pdf = _service.Create(request);
            return File(pdf, "application/pdf", "invoice.pdf");
        }
    }
}
