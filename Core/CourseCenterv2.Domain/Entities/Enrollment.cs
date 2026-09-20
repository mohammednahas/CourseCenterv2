namespace CourseCenterv2.Domain.Entities
{
    public class Enrollment
    {
        public int Id { private set; get; }
        public int StudentId { private set; get; }
        public int CourseId { private set; get; }
        public DateTime EnrolledAt { private set; get; }

        public Student Student { private set; get; }

        public Course Course { private set; get; }

        private Enrollment()
        {

        }

        internal  Enrollment(Student student, Course course)
        {
            Student = student;
            Course = course;

            EnrolledAt =DateTime.UtcNow;

        }


    }
}