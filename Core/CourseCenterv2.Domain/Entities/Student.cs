namespace CourseCenterv2.Domain.Entities;

public class Student
{
    private readonly List<Enrollment> _enrollments = new();

    public int Id { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Email { get; set; }

    public string? Mobile { get; private set; }

    public virtual IReadOnlyCollection<Enrollment> Enrollments
        => _enrollments.AsReadOnly();

    protected Student()
    {
        // Required by EF Core and Lazy Loading Proxy
    }

    public Student(
        string firstName,
        string lastName,
        string email,
        string? mobile)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetEmail(email);
        SetMobile(mobile);
    }

    public void Rename(string firstName, string lastName)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
    }

    public void ChangeEmail(string email)
    {
        SetEmail(email);
    }

    public void ChangeMobile(string? mobile)
    {
        SetMobile(mobile);
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

    private void SetFirstName(string firstName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException(
                "Student first name is required.",
                nameof(firstName));

        FirstName = firstName.Trim();
    }

    private void SetLastName(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException(
                "Student last name is required.",
                nameof(lastName));

        LastName = lastName.Trim();
    }

    private void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException(
                "Student email is required.",
                nameof(email));

        Email = email.Trim();
    }

    private void SetMobile(string? mobile)
    {
        Mobile = string.IsNullOrWhiteSpace(mobile)
            ? null
            : mobile.Trim();
    }
}