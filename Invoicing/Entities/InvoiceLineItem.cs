using System.ComponentModel.DataAnnotations;

namespace Invoicing.Entities
{
    public class InvoiceLineItem
    {
        public int InvoiceLineItemId { get; set; }

        public double? Amount { get; set; }

        public string? Description { get; set; }

        // FK:
        public int? InvoiceId { get; set; }

        public Invoice? Invoice { get; set; }
    }
}
