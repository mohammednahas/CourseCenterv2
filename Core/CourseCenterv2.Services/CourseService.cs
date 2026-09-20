using CourseCenterv2.Domain.Entities;

namespace CourseCenterv2.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task ChangeCapacityAsync(int id, int capacity)
        {
            var cours = await _courseRepository.GetByIdAsync(id);
            cours.ChangeCapacity(capacity);
           await _courseRepository.UpdateAsync(cours);
        }

        public async Task ChangeTitleAsync(int id, string Title)
        {
            var cours = await _courseRepository.GetByIdAsync(id);
            cours.ChangeTitle(Title);

           await _courseRepository.UpdateAsync(cours);

        }

        public async Task CreateAsync(string Title, int capacity)
        {
            var course = new Course(Title, capacity);
            await _courseRepository.AddAsync(course);

        }

        public async Task DeleteAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            await _courseRepository.DeleteAsync(course);
        }

        public async Task<IReadOnlyCollection<Course>> GetAllAsync()
        {
            return await _courseRepository.GetAllAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _courseRepository.GetByIdAsync(id);
        }
    }
}