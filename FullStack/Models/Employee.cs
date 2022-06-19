namespace FullStack.Models
{
    public class Employee
    {
        public int? EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Department { get; set; }

        public string? DateOfJoining { get; set; }

        public IFormFile? PhotoFile { get; set; }
        public byte[]? PhotoFileName { get; set; }

        public List <IFormFile>? Files { get; set; }
        public byte[]? FileUp { get; set; }
    }
}
