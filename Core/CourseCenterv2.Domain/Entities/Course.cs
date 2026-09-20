namespace CourseCenterv2.Domain.Entities;

public class Course
{
    private readonly List<Enrollment> _enrollments = new();

    public int Id { get; private set; }
    public string Title { get; private set; }
    public int Capacity { get; private set; }

    public virtual IReadOnlyCollection<Enrollment> Enrollments
        => _enrollments.AsReadOnly();

    protected Course()
    {
        // Required by EF Core and Lazy Loading Proxy
    }

    public Course(string title, int capacity)
    {
        SetTitle(title);
        SetCapacity(capacity);
    }

    public void ChangeTitle(string title)
    {
        SetTitle(title);
    }

    public void ChangeCapacity(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException(
                "Capacity must be greater than zero.",
                nameof(capacity));

        if (capacity < _enrollments.Count)
            throw new InvalidOperationException(
                "Capacity cannot be less than current enrollment count.");

        Capacity = capacity;
    }

    public bool HasAvailableSeat()
    {
        return _enrollments.Count < Capacity;
    }

    internal void AddEnrollment(Enrollment enrollment)
    {
        ArgumentNullException.ThrowIfNull(enrollment);

        if (_enrollments.Any(e => e.Id == enrollment.Id))
            return;

        _enrollments.Add(enrollment);
    }

    private void SetTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(
                "Course title is required.",
                nameof(title));

        Title = title.Trim();
    }

    private void SetCapacity(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException(
                "Capacity must be greater than zero.",
                nameof(capacity));

        Capacity = capacity;
    }
}