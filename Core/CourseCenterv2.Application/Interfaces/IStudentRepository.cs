using CourseCenterv2.Domain.Entities;

namespace CourseCenterv2.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student?> GetByIdAsync(int id);

        Task<IReadOnlyList<Student?>> GetAllAsync();
        Task AddAsync(Student student);

        Task UpdateAsync(Student student);

        Task DeleteAsync(Student student);

    }
}