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
    public class CustomerByGroupViewModel : BaseViewModel
    {
        public Customer? Customer { get; set; }
        public Invoice? Invoice { get; set; }
		public bool IsNewCustomer { get; set; }
		public char LowerBound
		{
			get
			{
				char firstLetter = char.ToUpper(Customer?.Name?.FirstOrDefault() ?? 'A'); // Use default 'A' if Customers or Name is null
				return DetermineLowerBound(firstLetter);
			}
		}

		public char UpperBound
		{
			get
			{
				char firstLetter = char.ToUpper(Customer?.Name?.FirstOrDefault() ?? 'A'); // Use default 'A' if Customers or Name is null
				return DetermineUpperBound(firstLetter);
			}
		}

		private char DetermineLowerBound(char firstLetter)
		{
			return firstLetter;
		}

		private char DetermineUpperBound(char firstLetter)
		{
			return firstLetter;
		}
	}

}
