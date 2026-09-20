using CourseCenterv2.Application.Interfaces;
using CourseCenterv2.Domain.Entities;

namespace CourseCenterv2.Services
{
    public class StudentService : IStudentService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;

        public StudentService()
        {
        }

        public StudentService(IStudentRepository studentRepository,ICourseRepository courseRepository,IEnrollmentRepository enrollmentRepository)
        {
            _studentRepository = studentRepository;
            _courseRepository=courseRepository;
            _enrollmentRepository=enrollmentRepository;
        }

        public async Task CreateAsync(string name)
        {
            var student = new Student(name);
            await _studentRepository.AddAsync(student);
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student is null)
            {
                throw new InvalidOperationException("This Student not found");
            }

            await _studentRepository.DeleteAsync(student);
        }

        public async Task EnrollInCourseAsync(int studentid, int Courseid)
        {
           

            var student = await _studentRepository.GetByIdAsync(studentid);

          
            var course = await _courseRepository.GetByIdAsync(Courseid);

            var enrollment = student.EnrollIn(course);

            await _enrollmentRepository.AddAsync(enrollment);
        }

        public async Task<IReadOnlyList<Student?>> GetAllAsync()
        {
            var students = _studentRepository.GetAllAsync();
            return await students;
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _studentRepository.GetByIdAsync(id);

        }

        public async Task RenameAsync(int id, string newName)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student is null)
            {

                throw new InvalidOperationException("This Student not found");
            }

            student.Rename(newName);

            await _studentRepository.UpdateAsync(student);

        }
    }
}