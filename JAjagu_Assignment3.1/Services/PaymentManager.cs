/*
 * Programmed by : Johnstanley Ajagu
 * Student Id: 8864315
 * Revision history:
 *      23-nov-2023: Project created
 *      14-nov-2023: project completed
 */

using Invoicing.Entities;
using Invoicing.Services;
using JAjagu_Assignment3._1.DataAccess;
using Microsoft.EntityFrameworkCore;
using JAjagu_Assignment3._1.Models;
using System.Diagnostics;
using System;

namespace JAjagu_Assignment3._1.Services
{
	public class PaymentManager : IPaymentManger
	{
		private PaymentDbContext _paymentDbContext;
		public PaymentManager(PaymentDbContext paymentDbContext)
		{
			_paymentDbContext = paymentDbContext;
		}

		public List<Customer> GetCustomerByGroup(string lowerBound = "A", string upperBound = "Z")
		{
			var customers = _paymentDbContext.Customers
				.Include(c => c.Invoices)
				.Where(c => c.IsDeleted == false
					&& c.Name.ToLower().Substring(0, 1).CompareTo(lowerBound) >= 0
					&& c.Name.ToLower().Substring(0, 1).CompareTo(upperBound) <= 0)
				.OrderBy(m => m.Name).ToList();

			return customers;
		}

		public int AddCustomer(Customer customer)
		{
			_paymentDbContext.Customers.Add(customer);
			_paymentDbContext.SaveChanges();
			return customer.CustomerId;
		}

		public void UpdateCustomer(Customer customer)
		{
			_paymentDbContext.Customers.Update(customer);
			_paymentDbContext.SaveChanges();
		}

		public List<Invoice> GetAllInvoices()
		{
			var invoices = _paymentDbContext.Invoices
				.Include(i => i.InvoiceLineItems)
				.Include(p => p.PaymentTerms)
				.ToList();
			return invoices;	
		}

		public List<PaymentTerms> GetAllPaymentTerms()
		{
			var paymentTerms = _paymentDbContext.PaymentTerms
				.ToList();
			return paymentTerms;
		}

		public Customer? GetCustomerByInvoiceId(int invoiceId)
		{
			throw new NotImplementedException();
		}

		public Customer GetCustomersById(int customerId)
		{
			Customer? customer = _paymentDbContext.Customers
			   .Include(c => c.Invoices)
			   .FirstOrDefault(c => c.CustomerId == customerId);
			return customer;
		}

		public Invoice GetInvoiceById(int customerId)
		{
			Invoice? invoice = _paymentDbContext.Invoices
				.Include(i => i.InvoiceLineItems)
				.Include(p => p.PaymentTerms)
				.FirstOrDefault(c => c.CustomerId == customerId);
			return invoice;
		}

		public PaymentTerms? GetPaymentTermsById(int paymentTermsId)
		{
			PaymentTerms? terms = _paymentDbContext.PaymentTerms
				.FirstOrDefault(pt => pt.PaymentTermsId == paymentTermsId);

			return terms;
		}

		public PaymentTerms? GetPaymentTermsByInvoiceId(int invoiceId)
		{
			PaymentTerms? terms = _paymentDbContext.Invoices
				.Where(i => i.InvoiceId == invoiceId)
				.Select(i => i.PaymentTerms)
				.FirstOrDefault();

			return terms;
		}


		public List<Invoice> GetInvoiceByCustomerId(int customerId)
		{
			var customerInvoices = _paymentDbContext.Invoices
				.Where (c => c.CustomerId == customerId)
				.ToList();

			foreach (var customerInvoice in customerInvoices)
			{
				Debug.WriteLine($"Invoice ID: {customerInvoice.InvoiceId}," +
					$" Invoice due date: {customerInvoice.InvoiceDueDate}, Amount paid {customerInvoice.PaymentTotal}" );
			}
			return customerInvoices;
		}


		public List<InvoiceLineItem> GetInvoiceLineItemsById(int invoiceId)
		{
			var lineItems = _paymentDbContext.InvoiceLineItems
				.Where(li => li.InvoiceId == invoiceId)
				.ToList();

			foreach (var lineItem in lineItems)
			{
				Debug.WriteLine($"Line Item: {lineItem.Description}, Amount: {lineItem.Amount}");
			}

			return lineItems;
		}

		public List<PaymentTerms> GetPaymentTerms()
		{
			var paymentTerms = _paymentDbContext.PaymentTerms
				.Include(pt => pt.Invoices)
				.ToList();
			return paymentTerms;
		}


		public void SoftDeleteCustomer(int customerId, CustomerByGroupViewModel viewModel)
		{
			var customer = _paymentDbContext.Customers
			   .Include(c => c.Invoices)
			   .FirstOrDefault(c => c.CustomerId == customerId);

			if (customer != null)
			{
				customer.IsDeleted = true;
				_paymentDbContext.SaveChanges();
			}
		}

		public void UndoSoftDeleteCustomer(int customerId)
		{
			var customer = _paymentDbContext.Customers
			   .Include(c => c.Invoices)
			   .FirstOrDefault(c => c.CustomerId == customerId);

			if (customer != null)
			{
				customer.IsDeleted = false;
				_paymentDbContext.SaveChanges();
			}
		}

		public void AddLineItemsToInvoice(int invoiceId, CustomerInvoicesViewModel viewModel)
		{
			var lineItems = _paymentDbContext.Invoices
					.Include(c => c.InvoiceLineItems)
					.FirstOrDefault(c => c.InvoiceId == invoiceId);

			if (lineItems != null && viewModel.InvoiceLineItems != null)
			{
				lineItems.InvoiceLineItems?.Add(viewModel.InvoiceLineItems);

				viewModel.InvoiceLineItems.InvoiceId = lineItems.InvoiceId;

				_paymentDbContext.SaveChanges();
			}
		}

		public void AddInvoiceToCustomer(int customerId, CustomerInvoicesViewModel viewModel)
		{
			var invoice = _paymentDbContext.Customers
					.Include(c => c.Invoices)
					.FirstOrDefault(c => c.CustomerId == customerId);

			if (invoice != null && viewModel.Invoices != null)
			{
				invoice.Invoices?.Add(viewModel.Invoices);

				viewModel.Invoices.CustomerId = invoice.CustomerId;

				_paymentDbContext.SaveChanges();
			}
		}
	}
}
