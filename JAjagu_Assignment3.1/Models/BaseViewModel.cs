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
	public class BaseViewModel
	{
		public List<Invoice>? ActiveInvoices { get; set; }
		public List<Customer>? ActiveCustomers { get; set; }
		public List<InvoiceLineItem>? ActiveLineItems { get; set; }
		public List<PaymentTerms>? ActivePaymentTerms { get; set; }
		public string? ActivePage { get; set; } = "A - Z";

	}
}
