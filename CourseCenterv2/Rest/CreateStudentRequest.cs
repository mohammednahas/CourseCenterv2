
namespace CourseCenterv2.Rest;

public class CreateStudentRequest
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string SSN { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}
