namespace CourseCenterv2.Domain.Entities
{
    public class Course
    {
        public int Id{private set;get;}

        public String Title {private set;get;}

        public int Capacity {private set;get;}

        private readonly List<Enrollment> _enrollments=new();
        public IReadOnlyCollection<Enrollment> Enrollments=>_enrollments.AsReadOnly();

        private Course()
        {
            
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

        public void SetTitle(string title)
        {
            Title=title.Trim();
        }

        public void ChangeCapacity(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException(
                    "Capacity must be greater than 0 ",nameof(capacity)
                );
            }

            if (capacity < _enrollments.Count())
            {
                throw new InvalidOperationException(
                    "this cours if full"
                );
                
            }

            Capacity=capacity;
        }

        public bool HasSeat()
        {
            return _enrollments.Count()<= Capacity;
        }

        internal void AddEnrollment(Enrollment enrollment)
        {
            ArgumentNullException.ThrowIfNull(enrollment);

            _enrollments.Add(enrollment);
        }

        public void SetCapacity(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException(
                    "Capacity must be greater than 0 ",nameof(capacity)
                );
            }
            Capacity=capacity;

            
        }



    }
}