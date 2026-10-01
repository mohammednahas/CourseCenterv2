
namespace CourseCenterv2.Domain.Entities;

public class Student
{
    private readonly List<Enrollment> _enrollments = new();

    public int Id { get; private set; }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string Email { get; set; }

    public string Address { get; private set; }

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
        string address)
    {
        SetFirstName(firstName);
        SetLastName(lastName);
        SetEmail(email);
        SetAddress(address);
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

    public void ChangeAddress(string address)
    {
        SetAddress(address);
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

    private void SetAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException(
                "Student address is required.",
                nameof(address));

        Address = address.Trim();
    }
}