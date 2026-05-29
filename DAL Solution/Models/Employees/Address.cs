namespace DAL_Solution.Models.Employees
{
    public class Address
    {
        public string Country { get; set; } = null!;
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? State { get; set; }
        public string? ZipCode { get; set; }
    }
}
