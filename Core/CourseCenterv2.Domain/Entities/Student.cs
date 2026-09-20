namespace CourseCenterv2.Domain.Entities
{
    public class Student
    {
        public int Id { private set; get; }
        public string Name { private set; get; }

        private readonly List<Enrollment> _enrollments = new();
        public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();

        private Student()
        {

        }

        public Student(String name)
        {
            SetName(name);
        }

        public void SetName(String name)
        {
            Name = name.Trim();
        }

        public void Rename(String name)
        {
            SetName(name);
        }

        public Enrollment EnrollIn(Course course)
        {
            var enrollment = new Enrollment(this, course);
            _enrollments.Add(enrollment);
            course.AddEnrollment(enrollment);
            return enrollment;

        }
    }
}