namespace CourseCenterv2.Domain.Entities
{
    public class Student
    {
        public int Id { private set; get; }
        public string Name { private set; get; }

        public virtual ICollection<Enrollment> Enrollments { get; private set; }
    = new List<Enrollment>();

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
            Enrollments.Add(enrollment);
            course.AddEnrollment(enrollment);
            return enrollment;

        }
    }
}