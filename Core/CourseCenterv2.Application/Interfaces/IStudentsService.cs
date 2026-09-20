using CourseCenterv2.Domain.Entities;

namespace CourseCenterv2.Application.Interfaces
{
    public interface IStudentService
    {
        Task <Student?> GetByIdAsync(int id);

        Task<IReadOnlyList<Student?>> GetAllAsync();
        Task CreateAsync(String name);

        Task RenameAsync(int id,string newName);

        Task DeleteAsync(int id);

        Task EnrollInCourseAsync(int studentid,int Courseid);

    }
}