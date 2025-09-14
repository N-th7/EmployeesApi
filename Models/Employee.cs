namespace AvancApi.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }

        public string? DocumentId { get; set; }
        public string? Company { get; set; }

        public decimal MaxFixedAmount { get; set; }
    }
}
