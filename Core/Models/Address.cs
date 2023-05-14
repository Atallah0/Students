namespace Core.Models
{
    public class Address : BaseModel
    {
        public string? PhysicalAddress { get; set; }
        public string? PostalAddress { get; set; }

        //Related
        public int StudentId { get; set; }
    }
}