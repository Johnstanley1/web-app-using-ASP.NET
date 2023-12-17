using System.ComponentModel.DataAnnotations;

namespace Invoicing.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage ="Customer name is required to add a customer")]
        public string Name { get; set; } = null!;


        [Required(ErrorMessage ="Please enter your most recent address")]
        public string? Address1 { get; set; }


		[Required(ErrorMessage = "Please enter your secondary address in the format PO Box 12345")]
		public string? Address2 { get; set; }


		[Required(ErrorMessage = "Please enter your city of residence")]
		public string? City { get; set; } = null!;

	
		[Required(ErrorMessage = "Please enter your province or state")]
		[RegularExpression(@"^[a-zA-Z]{2}$", ErrorMessage = "Please enter a valid two-letter state or province code.")]
		public string? ProvinceOrState { get; set; } = null!;


		[Required(ErrorMessage = "Please enter your zip or postal code")]
		[RegularExpression(@"^((\d{5}-\d{4})|(\d{5})|([A-Z]\d[A-Z]\s\d[A-Z]\d))$", ErrorMessage = "Please enter a valid US ZIP code or Canadian postal code in the format A1A 1A1.")]
		public string? ZipOrPostalCode { get; set; } = null!;


		[Required(ErrorMessage = "Please enter your phone number")]
		[RegularExpression(@"^\d{3}-\d{3}-\d{4}$", ErrorMessage = "Please enter a valid phone number in the format 'XXX-XXX-XXXX'.")]
		public string? Phone { get; set; }


		[Required(ErrorMessage = "Company contact last name is required")]
		public string? ContactLastName { get; set; }


		[Required(ErrorMessage = "Company contact first name is required")]
		public string? ContactFirstName { get; set; }


		[Required(ErrorMessage = "Company contact email is required")]
		[EmailAddress(ErrorMessage ="Email must be in the format abc@domain.com")]
		public string? ContactEmail { get; set; }

        public bool IsDeleted { get; set; } = false;

        public ICollection<Invoice>? Invoices { get; set; }
    }
}
