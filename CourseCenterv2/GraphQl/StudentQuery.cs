using CourseCenterv2.Application.Interfaces;
using CourseCenterv2.Domain.Entities;

namespace CourseCenterv2.GraphQL;

public class StudentQuery
{
    public async Task<IEnumerable<Student>> GetStudents(
        IStudentService studentService)
    {
        var students = await studentService.GetAllAsync();

        return students.OfType<Student>();
    }
}