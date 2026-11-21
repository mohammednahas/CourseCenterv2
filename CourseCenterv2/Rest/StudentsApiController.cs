using CourseCenterv2.Application.Interfaces;
using CourseCenterv2.Domain.Entities;
using CourseCenterv2.Rest;
using Microsoft.AspNetCore.Mvc;

namespace CourseCenterv2.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsApiController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentsApiController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStudents()
        {
            var students = await _studentService.GetAllAsync();
            var result = students
                .OfType<Student>()
                .Select(student => new
                {
                    student.Id,
                    student.FirstName,
                    student.LastName,
                    student.Email
                });

            return Ok(result);


        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            //return Ok(new { message = "Endpoint reached", id });
            var student = await _studentService.GetByIdAsync(id);

            if (student is null)
            {
                return NotFound();
            }

            var result = new
            {
                student.Id,
                student.FirstName,
                student.LastName,
                student.Email
            };

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> CreateStudent(
            [FromBody] CreateStudentRequest request)
        {
            await _studentService.CreateAsync(
                request.FirstName,
                request.LastName,
                request.Email,
                request.SSN,
                request.Address);

            return Ok(new
            {
                message = "Student created successfully"
            });
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateStudent(
            int id,
            [FromBody] CreateStudentRequest request)
        {
            var student = await _studentService.GetByIdAsync(id);

            if (student is null)
            {
                return NotFound();
            }

            await _studentService.RenameAsync(
                id,
                request.FirstName,
                request.LastName);

            await _studentService.ChangeEmailAsync(
                id,
                request.Email);

            await _studentService.ChangeSSNAsync(
                id,
                request.SSN);

            await _studentService.ChangeAddressAsync(
                id,
                request.Address);

            
            return Ok(new
            {
                message = "Student Updated successfully"
            });
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _studentService.GetByIdAsync(id);

            if (student is null)
            {
                return NotFound();
            }

            await _studentService.DeleteAsync(id);

            return Ok(new
            {
                message = "Student Deleted successfully"
            });
        }




    }
}