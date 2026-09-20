using CourseCenterv2.Application.Interfaces;
using CourseCenterv2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseCenterv2.Services
{
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;

        public CourseService(IGenericRepository<Course> courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task ChangeCapacityAsync(int id, int capacity)
        {
            var course = await _courseRepository
                .Query()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (course is null)
            {
                throw new InvalidOperationException(
                    "This Course not found");
            }

            course.ChangeCapacity(capacity);

            await _courseRepository.SaveChangesAsync();
        }

        public async Task ChangeTitleAsync(int id, string title)
        {
            var course = await _courseRepository
                .Query()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (course is null)
            {
                throw new InvalidOperationException(
                    "This Course not found");
            }

            course.ChangeTitle(title);

            await _courseRepository.SaveChangesAsync();
        }

        public async Task CreateAsync(string title, int capacity)
        {
            var course = new Course(title, capacity);

            await _courseRepository.AddAsync(course);
            await _courseRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);

            if (course is null)
            {
                throw new InvalidOperationException(
                    "This Course not found");
            }

            _courseRepository.Delete(course);

            await _courseRepository.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Course>> GetAllAsync()
        {
            return await _courseRepository
                .Query()
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _courseRepository
                .Query()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}