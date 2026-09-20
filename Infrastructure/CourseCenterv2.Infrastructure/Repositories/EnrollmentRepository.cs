using CourseCenterv2.Domain.Entities;
using CourseCenterv2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseCenterv2.Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {

        private readonly AppDbContext _context;

        public EnrollmentRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Enrollment enrollment)
        {
            await _context.Enrollments.AddAsync(enrollment);
            await _context.SaveChangesAsync();
        }

        public void Delete(Enrollment enrollment)
        {
            _context.Enrollments.Remove(enrollment);
            _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Enrollment>> GetAllAsync()
        {
            return await _context.Enrollments.ToListAsync();
        }

        public async Task<Enrollment?> GetByIdaSync(int id)
        {
            return await _context.Enrollments.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}