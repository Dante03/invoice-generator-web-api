using invoice_generator_web_api.Domain.Models;

namespace invoice_generator_web_api.Services
{
    public static class InvoiceCalculator
    {
        public static InvoiceTotals Calculate(
            List<InvoiceItem> items,
            decimal tax,
            decimal discount,
            string type
        )
        {
            decimal subtotal = 0;
            decimal discountTotal = 0;
            decimal taxTotal = 0;

            foreach (var item in items)
            {
                var baseAmount = item.Price * item.Quantity;
                var discountAmount = baseAmount * (discount / 100);

                decimal taxAmount = type == "SERVICE"
                    ? (baseAmount - discountAmount) * tax
                    : baseAmount * tax;

                subtotal += baseAmount;
                discountTotal += discountAmount;
                taxTotal += taxAmount;
            }

            return new InvoiceTotals
            {
                Subtotal = subtotal,
                Discount = discount,
                Tax = tax * 100,
                Total = subtotal - discountTotal + taxTotal
            };
        }
    }

}
