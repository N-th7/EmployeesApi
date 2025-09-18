namespace AvancApi.Models
{
    public class Company
    {
        public int Id { get; set; }
        public required string CompanyName { get; set; }
        public required string TaxIdNumber { get; set; }
        public required string LegalRepresentativeName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
    }
}