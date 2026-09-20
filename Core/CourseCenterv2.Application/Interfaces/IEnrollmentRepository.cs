using CourseCenterv2.Domain.Entities;

public interface IEnrollmentRepository
{
    Task<Enrollment?> GetByIdaSync(int id);
    Task AddAsync(Enrollment enrollment);
    void Delete(Enrollment enrollment);
    Task<IReadOnlyCollection<Enrollment>> GetAllAsync();
}