using System.ComponentModel.DataAnnotations;

namespace Invoicing.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        [Required(ErrorMessage ="PLease enter an invoice date")]
        public DateTime? InvoiceDate { get; set; }

		public DateTime? InvoiceDueDate
        {
            get
            {
                return InvoiceDate?.AddDays(Convert.ToDouble(PaymentTerms?.DueDays));
            }
        }

        public double? PaymentTotal { get; set; } = 0.0;

        public DateTime? PaymentDate { get; set; }

        // FK:
        public int PaymentTermsId { get; set; }

        // FK:
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

		public PaymentTerms? PaymentTerms { get; set; }

        public ICollection<InvoiceLineItem>? InvoiceLineItems { get; set; }
    }
}