namespace FullStack.Dto
{
    public class EmployeeForCreation
    {
        public string EmployeeName { get; set; }
        public string Department { get; set; }
        public string? DateOfJoining { get; set; }
        public IFormFile? PhotoFile { get; set; }
        public byte[]? PhotoFileName { get; set; }

    }
}
