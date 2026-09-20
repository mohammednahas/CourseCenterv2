using CourseCenterv2.Domain.Entities;
using CourseCenterv2.Application.Interfaces;

using Microsoft.EntityFrameworkCore;
using CourseCenterv2.Infrastructure.Data;

namespace CourseCenterv2.Infrastructure.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Course course)
        {
            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(Course course)
        {
            _context.Courses.Remove(course);
           await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Course>> GetAllAsync()
        {
            var courses = await _context.Courses.ToListAsync();
            return courses;
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _context.Courses.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }
    }
}