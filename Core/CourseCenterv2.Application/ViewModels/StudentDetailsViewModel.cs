using CourseCenterv2.Domain.Entities;

namespace CourseCenterv2.Application.ViewModels;

public class StudentDetailsViewModel
{
    public Student Student { get; set; } = null!;

    public IReadOnlyList<Course> Courses { get; set; }
        = new List<Course>();
}