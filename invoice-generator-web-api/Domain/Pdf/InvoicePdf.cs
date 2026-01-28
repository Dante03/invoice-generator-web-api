using invoice_generator_web_api.Domain.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace invoice_generator_web_api.Domain.Pdf
{
    public class InvoicePdf : IDocument
    {
        private readonly Invoice _invoice;

        public InvoicePdf(Invoice invoice)
        {
            _invoice = invoice;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Size(PageSizes.A4);
                page.DefaultTextStyle(x => x.FontSize(10));

                page.Content().Column(col =>
                {
                    col.Spacing(25);

                    col.Item().Element(Header);
                    col.Item().Element(ItemsTable);
                    col.Item().Element(FooterSection);
                });
            });
        }

        void Header(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem(2).Column(col =>
                {
                    col.Item().Text("Invoice")
                        .FontSize(28)
                        .Bold();

                    col.Item().Text("Your Company")
                        .FontSize(12)
                        .SemiBold()
                        .FontColor(Colors.Grey.Darken1);

                    col.Item().Text(_invoice.Seller.Name);
                    col.Item().Text(_invoice.Seller.Address);
                    col.Item().Text($"{_invoice.Seller.City}, {_invoice.Seller.Country}");
                    col.Item().Text(_invoice.Seller.Email);

                    col.Item().PaddingTop(15).Text("Client's Company")
                        .FontSize(12)
                        .SemiBold()
                        .FontColor(Colors.Grey.Darken1);

                    col.Item().Text(_invoice.Client.Name);
                    col.Item().Text(_invoice.Client.Address);
                    col.Item().Text($"{_invoice.Client.City}, {_invoice.Client.Country}");
                    col.Item().Text(_invoice.Client.Email);
                });

                row.RelativeItem(1).AlignRight().Column(col =>
                {
                    col.Item().Border(1)
                        .BorderColor(Colors.Grey.Lighten2)
                        .Padding(10)
                        .AlignCenter()
                        .Text("");

                    col.Item().PaddingTop(20).Text($"Invoice No: {_invoice.InvoiceNumber}");
                    col.Item().Text($"Invoice Date: {_invoice.InvoiceDate:MM/dd/yyyy}");
                    col.Item().Text($"Due Date: {_invoice.DueDate:MM/dd/yyyy}");
                });
            });
        }

        void ItemsTable(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(cols =>
                {
                    cols.ConstantColumn(40);
                    cols.RelativeColumn();
                    cols.ConstantColumn(80);
                    cols.ConstantColumn(80);
                });

                table.Header(header =>
                {
                    header.Cell().Element(HeaderCell).Text("ID");
                    header.Cell().Element(HeaderCell).Text("Description");
                    header.Cell().Element(HeaderCell).AlignCenter().Text("Quantity");
                    header.Cell().Element(HeaderCell).AlignRight().Text("Price");
                });

                int index = 1;
                foreach (var item in _invoice.Items)
                {
                    table.Cell().Text(index++.ToString());
                    table.Cell().Text(item.Description);
                    table.Cell().AlignCenter().Text(item.Quantity.ToString());
                    table.Cell().AlignRight().Text(item.Price.ToString("0.00"));
                }
            });
        }

        static IContainer HeaderCell(IContainer container)
        {
            return container
                .Background(Colors.Black)
                .Padding(8)
                .DefaultTextStyle(x => x.FontColor(Colors.White).SemiBold());
        }

        void FooterSection(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().PaddingRight(20).Border(1).Padding(1).Column(col =>
                {
                    col.Item().Text("Notes").SemiBold();
                    col.Item().Text(_invoice.Notes ?? "Any additional comments")
                        .FontColor(Colors.Grey.Darken1);
                });

                row.ConstantItem(200).Column(col =>
                {
                    col.Item().Row(r =>
                    {
                        r.RelativeItem().Text("Subtotal:");
                        r.ConstantItem(80).AlignRight().Text(_invoice.Totals.Subtotal.ToString("0.00"));
                    });

                    col.Item().Row(r =>
                    {
                        r.RelativeItem().Text("Tax:");
                        r.ConstantItem(80).AlignRight().Text($"{_invoice.Totals.Tax}%");
                    });

                    col.Item().Row(r =>
                    {
                        r.RelativeItem().Text("Discount:");
                        r.ConstantItem(80).AlignRight().Text($"{_invoice.Totals.Discount}%");
                    });

                    col.Item().PaddingTop(5).LineHorizontal(1);

                    col.Item().Row(r =>
                    {
                        r.RelativeItem().Text("Total:").Bold();
                        r.ConstantItem(80).AlignRight()
                            .Text(_invoice.Totals.Total.ToString("0.00"))
                            .Bold();
                    });
                });
            });
        }
    }

}
