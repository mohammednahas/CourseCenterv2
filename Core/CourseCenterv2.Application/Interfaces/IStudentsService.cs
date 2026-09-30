using CourseCenterv2.Domain.Entities;

namespace CourseCenterv2.Application.Interfaces
{
    public interface IStudentService
    {
        Task<Student?> GetByIdAsync(int id);

        Task<IReadOnlyList<Student?>> GetAllAsync();

        Task CreateAsync(
            string firstName,
            string lastName,
            string email);

        Task RenameAsync(
            int id,
            string firstName,
            string lastName);

        Task ChangeEmailAsync(
            int id,
            string email);

        Task DeleteAsync(int id);

        Task EnrollInCourseAsync(
            int studentId,
            int courseId);
    }
}