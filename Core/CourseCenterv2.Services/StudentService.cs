using CourseCenterv2.Application.Interfaces;
using CourseCenterv2.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseCenterv2.Services;

public class StudentService : IStudentService
{
    private readonly IGenericRepository<Student> _studentRepository;
    private readonly IGenericRepository<Course> _courseRepository;
    private readonly IGenericRepository<Enrollment> _enrollmentRepository;

    public StudentService(
        IGenericRepository<Student> studentRepository,
        IGenericRepository<Course> courseRepository,
        IGenericRepository<Enrollment> enrollmentRepository)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task CreateAsync(string name)
    {
        var student = new Student(name);

        await _studentRepository.AddAsync(student);
        await _studentRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var student = await _studentRepository.GetByIdAsync(id);

        if (student is null)
        {
            throw new InvalidOperationException(
                "This Student not found");
        }

        _studentRepository.Delete(student);

        await _studentRepository.SaveChangesAsync();
    }

    public async Task EnrollInCourseAsync(int studentId, int courseId)
    {
        var student = await _studentRepository
            .Query()
            .Include(s => s.Enrollments)
            .FirstOrDefaultAsync(s => s.Id == studentId);

        var course = await _courseRepository
            .GetByIdAsync(courseId);

        if (student is null)
        {
            throw new InvalidOperationException(
                "This Student not found");
        }

        if (course is null)
        {
            throw new InvalidOperationException(
                "This Course not found");
        }

        var enrollment = student.EnrollIn(course);

        await _enrollmentRepository.AddAsync(enrollment);
        await _enrollmentRepository.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Student?>> GetAllAsync()
    {
        return await _studentRepository
            .Query()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _studentRepository
            .Query().Include(e => e.Enrollments).
            ThenInclude(c => c.Course)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task RenameAsync(int id, string newName)
    {
        var student = await _studentRepository
            .Query()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student is null)
        {
            throw new InvalidOperationException(
                "This Student not found");
        }

        student.Rename(newName);

        await _studentRepository.SaveChangesAsync();
    }
}