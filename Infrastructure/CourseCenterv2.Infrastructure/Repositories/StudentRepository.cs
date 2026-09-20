using CourseCenterv2.Application.Interfaces;
using CourseCenterv2.Domain.Entities;
using CourseCenterv2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CourseCenterv2.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student student)
        {
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

        }



        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students
            
            .FirstOrDefaultAsync(x => x.Id == id);


        }


       //eager loading 
        //  public async Task<Student?> GetByIdAsync(int id)
        // {
        //     return await _context.Students
        //     .Include(e=> e.Enrollments)
        //     .ThenInclude(c=>c.Course)
            
        //     .FirstOrDefaultAsync(x => x.Id == id);


        // }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);
             await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<Student?>> GetAllAsync()
        {
            var students = _context.Students.ToListAsync();
            return await students;
        }
    }
}