
using CourseCenterv2.Application.Interfaces;
using CourseCenterv2.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CourseCenterv2.Services;

public class StudentService : IStudentService
{
    private readonly IGenericRepository<Student> _studentRepository;
    private readonly IGenericRepository<Course> _courseRepository;
    private readonly IGenericRepository<Enrollment> _enrollmentRepository;
    private readonly IEmailService _emailService;
    private readonly ILogger<StudentService> _logger;

    public StudentService(
        IGenericRepository<Student> studentRepository,
        IGenericRepository<Course> courseRepository,
        IGenericRepository<Enrollment> enrollmentRepository,
        IEmailService emailService,
        ILogger<StudentService> logger)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
        _enrollmentRepository = enrollmentRepository;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task CreateAsync(
        string firstName,
        string lastName,
        string email,
        string ssn,
        string address)
    {
        var student = new Student(
            firstName,
            lastName,
            email,
            ssn,
            address);

        await _studentRepository.AddAsync(student);
        await _studentRepository.SaveChangesAsync();

        try
        {
            await _emailService.SendEmailAsync(
                email,
                "Welcome to CourseCenter",
                $"""
                Hello {firstName} {lastName},

                Welcome to CourseCenter!

                Your student registration has been completed successfully.

                Best regards,
                CourseCenter Team
                """);

            _logger.LogInformation(
                "Welcome email sent successfully to {Email}",
                email);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send welcome email to {Email}",
                email);
        }
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

    public async Task EnrollInCourseAsync(
        int studentId,
        int courseId)
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

        try
        {
            await _emailService.SendEmailAsync(
                student.Email,
                "Course Enrollment Confirmation",
                $"""
                Hello {student.FirstName} {student.LastName},

                You have been successfully enrolled in the course:
                {course.Title}

                Best regards,
                CourseCenter Team
                """);

            _logger.LogInformation(
                "Enrollment email sent successfully to {Email} for course {CourseTitle}",
                student.Email,
                course.Title);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to send enrollment email to {Email} for course {CourseTitle}",
                student.Email,
                course.Title);
        }
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
            .Query()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task RenameAsync(
        int id,
        string firstName,
        string lastName)
    {
        var student = await _studentRepository
            .Query()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student is null)
        {
            throw new InvalidOperationException(
                "This Student not found");
        }

        student.Rename(firstName, lastName);

        await _studentRepository.SaveChangesAsync();
    }

    public async Task ChangeEmailAsync(
        int id,
        string email)
    {
        var student = await _studentRepository
            .Query()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student is null)
        {
            throw new InvalidOperationException(
                "This Student not found");
        }

        student.ChangeEmail(email);

        await _studentRepository.SaveChangesAsync();
    }

    public async Task ChangeSSNAsync(
        int id,
        string ssn)
    {
        var student = await _studentRepository
            .Query()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student is null)
        {
            throw new InvalidOperationException(
                "This Student not found");
        }

        student.ChangeSSN(ssn);

        await _studentRepository.SaveChangesAsync();
    }

    public async Task ChangeAddressAsync(
        int id,
        string address)
    {
        var student = await _studentRepository
            .Query()
            .FirstOrDefaultAsync(x => x.Id == id);

        if (student is null)
        {
            throw new InvalidOperationException(
                "This Student not found");
        }

        student.ChangeAddress(address);

        await _studentRepository.SaveChangesAsync();
    }
}
