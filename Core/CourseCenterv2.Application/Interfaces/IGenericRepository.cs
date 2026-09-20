namespace CourseCenterv2.Application.Interfaces;

public interface IGenericRepository<T>
    where T : class
{
    IQueryable<T> Query();

    Task<T?> GetByIdAsync(int id);

    Task AddAsync(T entity);

    void Delete(T entity);

    Task SaveChangesAsync();
}