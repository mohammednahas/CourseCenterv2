namespace CourseCenterv2.Domain.Entities;

public class Student
{
    private readonly List<Enrollment> _enrollments = new();

    public int Id { get; private set; }
    public string Name { get; private set; }

    public string Email { get; set; }

    public virtual IReadOnlyCollection<Enrollment> Enrollments
        => _enrollments.AsReadOnly();

    protected Student()
    {
        // Required by EF Core and Lazy Loading Proxy
    }

    public Student(string name)
    {
        SetName(name);
    }

    public void Rename(string name)
    {
        SetName(name);
    }

    public Enrollment EnrollIn(Course course)
    {
        ArgumentNullException.ThrowIfNull(course);

        if (_enrollments.Any(e => e.CourseId == course.Id))
            throw new InvalidOperationException(
                "Student is already enrolled in this course.");

        

        var enrollment = new Enrollment(this, course);

        _enrollments.Add(enrollment);
        course.AddEnrollment(enrollment);

        return enrollment;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Student name is required.",
                nameof(name));

        Name = name.Trim();
    }
}