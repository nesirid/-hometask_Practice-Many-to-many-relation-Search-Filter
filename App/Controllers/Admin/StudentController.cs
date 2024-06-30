using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Students;
using Service.Services.Interface;

namespace App.Controllers.Admin
{
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _studentService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateDto request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Request cannot be null" });
            }

            await _studentService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), new { response = "Data successfully created" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] StudentEditDto request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Request cannot be null" });
            }

            var existingStudent = await _studentService.GetByIdAsync(id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            await _studentService.EditAsync(id, request);
            return Ok(new { response = "Data successfully updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingStudent = await _studentService.GetByIdAsync(id);
            if (existingStudent == null)
            {
                return NotFound();
            }

            await _studentService.DeleteAsync(id);
            return Ok(new { response = "Data successfully deleted" });
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] string name, [FromQuery] string surname, [FromQuery] int? age)
        {
            var result = await _studentService.FilterAsync(name, surname, age);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            var result = await _studentService.SearchAsync(name);
            return Ok(result);
        }

        [HttpGet("sort")]
        public async Task<IActionResult> Sort([FromQuery] string sortBy)
        {
            var result = await _studentService.SortAsync(sortBy);
            return Ok(result);
        }
    }
}
