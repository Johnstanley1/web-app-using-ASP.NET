/*
 * Programmed by : Johnstanley Ajagu
 * Student Id: 8864315
 * Revision history:
 *      23-nov-2023: Project created
 *      14-nov-2023: project completed
 */
using Invoicing.Entities;
using Invoicing.Services;
using JAjagu_Assignment3._1.Models;
using JAjagu_Assignment3._1.Services;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JAjagu_Assignment3._1.Controllers
{
	public class PaymentController : Controller
	{
		private IPaymentManger _paymentManager;

		public PaymentController(IPaymentManger paymentManager)
		{
			_paymentManager = paymentManager;
		}


		/// <summary>
		/// Method to render all customers in the view
		/// </summary>
		/// <param name="lowerBound"></param>
		/// <param name="upperBound"></param>
		/// <returns></returns>
		[HttpGet("customers/{lowerBound}-{upperBound}")]
		public IActionResult Customers(int id, string lowerBound, string upperBound)
		{
			List<CustomerByGroupViewModel> customerSummaries = _paymentManager.GetCustomerByGroup(lowerBound, upperBound)
				.Select(c => new CustomerByGroupViewModel()
				{	
					Customer = c,
					ActivePage = $"{lowerBound}-{upperBound}"
				})
				.ToList();

			return View("Customers", customerSummaries);
		}


		/// <summary>
		/// Get method to add customers to the list
		/// </summary>
		/// <returns></returns>
		[HttpGet("customers/add-customer")]
		public IActionResult AddCustomer()
		{
			AddCustomerViewModel viewModel = new AddCustomerViewModel()
			{
				GroupedCustomer = new Customer(),
				IsNewCustomer = true,
			};

			return View("AddCustomer", viewModel);
		}

		/// <summary>
		/// Post method to add customers to the list
		/// </summary>
		/// <returns></returns>
		[HttpPost("customers/add-customer")]
		public IActionResult AddCustomer(AddCustomerViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				_paymentManager.AddCustomer(viewModel.GroupedCustomer);
				TempData["LastActionMessage"] = $"The customer '{viewModel.GroupedCustomer.Name}' was added successfully";

				char lowerBound = viewModel.LowerBound;
				char upperBound = viewModel.UpperBound;

				// Redirect to the grouped customer list based on the appropriate bounds
				return RedirectToAction("Customers", new 
				{
					lowerBound,
					upperBound
				});
			}
			else
			{
				ModelState.AddModelError("", "There were errors in the form - please fix them and try again");
				return View("AddCustomer", viewModel);
			}
		}



		/// <summary>
		/// Get method to edit customers in the list
		/// </summary>
		/// <returns></returns>
		[HttpGet("/customers/{id}/edit-customer")]
		public IActionResult EditCustomer(int id)
		{
			var customers = _paymentManager.GetCustomersById(id);

			AddCustomerViewModel viewModel = new AddCustomerViewModel()
			{
				GroupedCustomer = customers,
				IsNewCustomer = false,
			};
			return View("EditCustomer", viewModel);
		}

		/// <summary>
		/// Post method to edit customers in the list
		/// </summary>
		/// <returns></returns>
		[HttpPost("/customers/{id}/edit-customer")]
		public IActionResult EditCustomer(AddCustomerViewModel customerViewModel)
		{
			if (ModelState.IsValid)
			{
				_paymentManager.UpdateCustomer(customerViewModel.GroupedCustomer);
				TempData["LastActionMessage"] = $"The customer '{customerViewModel.GroupedCustomer.Name}' was updated successfully";

				// Redirect to the grouped customer list based on the appropriate bounds
				char lowerBound = customerViewModel.LowerBound;
				char upperBound = customerViewModel.UpperBound;

				// Redirect to the grouped customer list based on the appropriate bounds
				return RedirectToAction("Customers", new
				{
					lowerBound,
					upperBound

					//customerViewModel.GroupRange

				});
			}
			else
			{
				ModelState.AddModelError("", "There were errors in the form - please fix them and try again");

				return View("EditCustomer", customerViewModel);
			}
		}


		/// <summary>
		/// Method to render customer invoice view
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet("customers/{id}/view-invoice")]
		public IActionResult CustomerInvoices(int id)
		{
			Customer customer = _paymentManager.GetCustomersById(id);
			List<Invoice> invoices = _paymentManager.GetInvoiceByCustomerId(id);
			List<InvoiceLineItem> invoiceLineItems = _paymentManager.GetInvoiceLineItemsById(id);
			List<PaymentTerms> paymentTerms = _paymentManager.GetAllPaymentTerms();
			PaymentTerms selectedPaymentTerm = _paymentManager.GetPaymentTermsByInvoiceId(id);

			CustomerInvoicesViewModel viewModel = new CustomerInvoicesViewModel()
			{
				Customers = customer,
				ActiveInvoices = invoices,
				Invoices = new Invoice(),
				ActiveLineItems = invoiceLineItems,
				InvoiceLineItems = new InvoiceLineItem(),
				PaymentTerms = selectedPaymentTerm,
				TotalLineItems = invoiceLineItems?.Sum(i => i.Amount),
				SelectedInvoiceId = id
			};

			return View("CustomerInvoices", viewModel);
		}


		/// <summary>
		/// Method to process customer invoices
		/// </summary>
		/// <returns></returns>
		[HttpPost("customers/{id}/view-invoice")]
		public IActionResult CustomerInvoices(int id, CustomerInvoicesViewModel viewModel)
		{
			if (ModelState.IsValid)
			{
				_paymentManager.AddInvoiceToCustomer(id, viewModel);
				_paymentManager.AddLineItemsToInvoice(id, viewModel);

                var invoices = _paymentManager.GetInvoiceById(id);
                var lineItems = _paymentManager.GetInvoiceLineItemsById(id);
                if (invoices != null)
				{
					TempData["LastActionMessage"] = $"The Invoice with id '{viewModel.Invoices?.InvoiceId}' was added successfully";
				}

                if (lineItems != null)
				{
                    TempData["LastActionMessage"] = $"The Invoice Line Item with id '{viewModel.InvoiceLineItems?.InvoiceLineItemId}' was added successfully";

                }
                return RedirectToAction("CustomerInvoices", new { id });
			}
			else
			{
				return View(viewModel);
			}
		}


		/// <summary>
		/// Method to delete customer from app
		/// </summary>
		/// <param name="id"></param>
		/// <param name="viewModel"></param>
		/// <returns></returns>
		[HttpPost("/customers/{id}/delete-customer")]
		public IActionResult DeleteCustomer(int id)
        {
			CustomerByGroupViewModel viewModel = new CustomerByGroupViewModel();
            _paymentManager.SoftDeleteCustomer(id, viewModel);

            // Store data for undo in TempData
            TempData["DeletedCustomerId"] = id;
            TempData["DeletedCustomerName"] = viewModel?.Customer?.Name;

            // Store the undo link in TempData
            TempData["UndoLink"] = Url.Action("UndoDelete", "Payment", new { id });

            // Store the confirmation message in TempData
            TempData["message"] = $"The customer '{viewModel?.Customer?.Name}' was deleted successfully.";

            // Redirect to the grouped customer list based on the appropriate bounds
            char lowerBound = viewModel.LowerBound;
			char upperBound = viewModel.UpperBound;

			// Redirect to the grouped customer list based on the appropriate bounds
			return RedirectToAction("Customers", new
			{
				lowerBound,
				upperBound

				//customerViewModel.GroupRange

			});
		}


		/// <summary>
		/// Method to undo the delete
		/// </summary>
		/// <param name="id"></param>
		/// <param name="viewModel"></param>
		/// <returns></returns>
		[HttpGet("/customers/undo-delete/{id}")]
		public IActionResult UndoDelete(int id, CustomerByGroupViewModel viewModel)
		{
			_paymentManager.UndoSoftDeleteCustomer(id);

            // Store the confirmation message in TempData
            if (viewModel?.Customer?.Name != null)
			{
                TempData["message"] = $"The customer '{viewModel.Customer.Name}' was restored successfully.";

            }

            // Redirect to the grouped customer list based on the appropriate bounds
            char lowerBound = viewModel.LowerBound;
			char upperBound = viewModel.UpperBound;

			// Redirect to the grouped customer list based on the appropriate bounds
			return RedirectToAction("Customers", new
			{
				lowerBound,
				upperBound

			});

		}



	}
}
	

