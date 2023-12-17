using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Invoicing.Entities;
using Xunit;

namespace InvoiceManagerTest
{
	public class UnitTestPayments
	{
		[Fact]
		public void Test_Average_Payment_Total()
		{
			//Arrange 
			Customer cus = new Customer()
			{
				Name = "Test",
				Address1 = "Conestoga College",
				City = "NB",
				Invoices = new List<Invoice>()

			};

			cus.Invoices.Add(new Invoice() { PaymentTotal = 100, PaymentDate = DateTime.Now });
			cus.Invoices.Add(new Invoice() { PaymentTotal = 500 , PaymentDate = DateTime.Now });

			// Act:
			double avgTotal = cus.Invoices.Average(r => r.PaymentTotal).GetValueOrDefault();

			// Assert:
			Assert.Equal(300, avgTotal);
		}



		[Fact]
		public void Count_Invoices()
		{
			// Arrange
			Invoice lineItem = new Invoice()
			{
				PaymentDate = DateTime.Now,
				PaymentTotal = 100,
				InvoiceLineItems = new List<InvoiceLineItem>()
			};


			lineItem.InvoiceLineItems.Add(new InvoiceLineItem() { Description = "Test3", InvoiceId = 1 });
			lineItem.InvoiceLineItems.Add(new InvoiceLineItem() { Description = "Test3", InvoiceId = 2 });

			// Act:
			double totalAmount = lineItem.InvoiceLineItems.Max(r => r.InvoiceId).GetValueOrDefault();

			// Assert:
			Assert.Equal(2, totalAmount);
		}



		[Fact]
		public void Sum_Invoice_LineItem_Amount()
		{
			// Arrange
			Invoice lineItem = new Invoice()
			{
				PaymentDate = DateTime.Now,
				PaymentTotal = 100,
				InvoiceLineItems = new List<InvoiceLineItem>()
			};


			lineItem.InvoiceLineItems.Add(new InvoiceLineItem() { Description = "Test3", Amount = 1500 });
			lineItem.InvoiceLineItems.Add(new InvoiceLineItem() { Description = "Test3", Amount = 1500 });

			// Act:
			double totalAmount = lineItem.InvoiceLineItems.Sum(r => r.Amount).GetValueOrDefault();

			// Assert:
			Assert.Equal(3000, totalAmount);
		}

	}
}
