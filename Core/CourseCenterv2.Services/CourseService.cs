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

        public async Task AddPrefix()
        {
            await _courseRepository
            .Query()
            .Where(s => s.Title.Contains("Programming"))
            .ExecuteUpdateAsync(U =>
             U.SetProperty(
                c => c.Title, c => "Advanced " + c.Title)

            );

        }
        public async Task DeleteProgrammingcourses()
        {
            await _courseRepository
            .Query()
            .Where(s => s.Title.Contains("Programming"))
            .ExecuteDeleteAsync();

        }

        public async Task UpdateAsync(int id, string title, int capacity, uint version)
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
            course.ChangeCapacity(capacity);

            var newVersion = version+1;

            var affectedRows = await _courseRepository
                .Query()
                .Where(x =>
                    x.Id == id &&
                    x.Version == version)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(x => x.Title, title)
                    .SetProperty(x => x.Capacity, capacity)
                    .SetProperty(x => x.Version, newVersion));

            if (affectedRows == 0)
            {
                throw new DbUpdateConcurrencyException(
                    "The course was modified by another user.");
            }
        }

    }
}