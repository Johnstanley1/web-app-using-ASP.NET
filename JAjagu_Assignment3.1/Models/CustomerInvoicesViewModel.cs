/*
 * Programmed by : Johnstanley Ajagu
 * Student Id: 8864315
 * Revision history:
 *      23-nov-2023: Project created
 *      14-nov-2023: project completed
 */

using Invoicing.Entities;

namespace JAjagu_Assignment3._1.Models
{
    public class CustomerInvoicesViewModel : BaseViewModel
    {
        public Customer? Customers { get; set; }
        public Invoice? Invoices { get; set; } // add new invoice
        public InvoiceLineItem? InvoiceLineItems { get; set; } // add new lineItems
        public PaymentTerms? PaymentTerms { get; set; }
        public double? TotalLineItems { get; set; }
		public int? SelectedInvoiceId {  get; set; }
		public string GroupRange
		{
			get
			{
				char firstLetter = char.ToUpper(Customers?.Name?.FirstOrDefault() ?? 'A'); // Use default 'A' if Customers or Name is null
				if (firstLetter >= 'A' && firstLetter <= 'E')
					return "A-E";
				else if (firstLetter >= 'F' && firstLetter <= 'K')
					return "F-K";
				else if (firstLetter >= 'L' && firstLetter <= 'R')
					return "L-R";
				else if (firstLetter >= 'S' && firstLetter <= 'Z')
					return "S-Z";
				else
					return "A-Z";  // Default to A-Z if the range is not determined
			}
		}

	}
}
