using Invoicing.Entities;

namespace JAjagu_Assignment3._1.Models
{
	public class AddCustomerViewModel
	{
		public Customer? GroupedCustomer { get; set; }
		public bool IsNewCustomer { get; set; }
		public string GroupRange
		{
			get
			{
				char firstLetter = char.ToUpper(GroupedCustomer?.Name?.FirstOrDefault() ?? 'A'); // Use default 'A' if Customers or Name is null
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

		public char LowerBound
		{
			get
			{
				char firstLetter = char.ToUpper(GroupedCustomer?.Name?.FirstOrDefault() ?? 'A'); // Use default 'A' if Customers or Name is null
				return DetermineLowerBound(firstLetter);
			}
		}

		public char UpperBound
		{
			get
			{
				char firstLetter = char.ToUpper(GroupedCustomer?.Name?.FirstOrDefault() ?? 'A'); // Use default 'A' if Customers or Name is null
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
