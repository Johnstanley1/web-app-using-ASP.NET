/*
 * Programmed by : Johnstanley Ajagu
 * Student Id: 8864315
 * Revision history:
 *      23-nov-2023: Project created
 *      14-nov-2023: project completed
 */

using Invoicing.Entities;
using JAjagu_Assignment3._1.Models;

namespace Invoicing.Services
{
	public interface IPaymentManger
	{
		public List<Customer> GetCustomerByGroup(string lowerBound = "A", string upperBound = "Z");
		public List<Invoice> GetAllInvoices();
		public Customer? GetCustomersById(int customerId);
		public Invoice? GetInvoiceById(int customerId);
		public PaymentTerms? GetPaymentTermsById(int paymentId);
		public List<Invoice>? GetInvoiceByCustomerId(int customerId);
		public List<InvoiceLineItem> GetInvoiceLineItemsById(int invoiceId);
		public PaymentTerms? GetPaymentTermsByInvoiceId(int invoiceId);
		public List<PaymentTerms> GetPaymentTerms();
		public int AddCustomer(Customer customer);
		public void UpdateCustomer(Customer customer);
		public void SoftDeleteCustomer(int customerId, CustomerByGroupViewModel viewModel);
		public void UndoSoftDeleteCustomer(int customerId);
		public void AddLineItemsToInvoice(int invoiceId, CustomerInvoicesViewModel viewModel);
		public void AddInvoiceToCustomer(int customerId, CustomerInvoicesViewModel viewModel);
		public List<PaymentTerms> GetAllPaymentTerms();



	}
}
