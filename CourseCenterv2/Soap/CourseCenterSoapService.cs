
using CourseCenterv2.Application.Interfaces;

namespace CourseCenterv2.Soap;

public class CourseCenterSoapService : ICourseCenterSoapService
{
    private readonly IStudentService _studentService;
    private readonly ICourseService _courseService;

    public CourseCenterSoapService(
        IStudentService studentService,
        ICourseService courseService)
    {
        _studentService = studentService;
        _courseService = courseService;
    }

    public async Task<List<StudentSoapDto>> GetAllStudentsAsync()
    {
        var students = await _studentService.GetAllAsync();

        return students
            .Where(s => s is not null)
            .Select(s => new StudentSoapDto
            {
                Id = s!.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email
            })
            .ToList();
    }

    public async Task<List<CourseSoapDto>> GetAllCoursesAsync()
    {
        var courses = await _courseService.GetAllAsync();

        return courses
            .Select(c => new CourseSoapDto
            {
                Id = c.Id,
                Title = c.Title,
                Capacity = c.Capacity
            })
            .ToList();
    }
}
