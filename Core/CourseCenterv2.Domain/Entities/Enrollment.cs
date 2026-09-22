namespace CourseCenterv2.Domain.Entities;

public class Enrollment
{
    public int Id { get; private set; }

    public int StudentId { get; private set; }
    public int CourseId { get; private set; }

    public DateTime EnrolledAt { get; private set; }

    public virtual Student Student { get; private set; }
    public virtual Course Course { get; private set; }

    protected Enrollment()
    {
        // Required by EF Core and Lazy Loading Proxy
    }

    internal Enrollment(Student student, Course course)
    {
        Student = student;
        Course = course;
        EnrolledAt = DateTime.UtcNow;
    }
}