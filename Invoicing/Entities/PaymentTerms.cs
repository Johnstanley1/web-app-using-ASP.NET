namespace Invoicing.Entities
{
    public class PaymentTerms
    {
        public int PaymentTermsId { get; set; }

        public string Description { get; set; } = null!;

        public int DueDays { get; set; }

        public ICollection<Invoice>? Invoices { get; set; }  
    }
}
