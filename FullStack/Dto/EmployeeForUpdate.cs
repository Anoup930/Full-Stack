namespace FullStack.Dto
{
    public class EmployeeForUpdate
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public bool IsPhotoNull { get; set; }
        public string Department { get; set; }
        public string? DateOfJoining { get; set; }
        public IFormFile? PhotoFile { get; set; }
        public byte[]? PhotoFileName { get; set; }
    }
}
