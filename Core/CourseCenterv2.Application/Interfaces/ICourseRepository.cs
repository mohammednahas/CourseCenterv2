using CourseCenterv2.Domain.Entities;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(int id);
    Task<IReadOnlyCollection<Course>> GetAllAsync();
    Task AddAsync(Course course);

    Task UpdateAsync(Course course);
    Task DeleteAsync(Course course);
}