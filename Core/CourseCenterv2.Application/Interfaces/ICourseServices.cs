using CourseCenterv2.Domain.Entities;

public interface ICourseService
{
    Task<Course?> GetByIdAsync(int id);
    Task<IReadOnlyCollection<Course>> GetAllAsync();
    Task CreateAsync(string Title, int capacity);
    Task ChangeTitleAsync(int id, string Title);
    Task ChangeCapacityAsync(int id, int capacity);

    Task DeleteAsync(int id);



}