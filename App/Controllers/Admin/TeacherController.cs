using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Teachers;
using Service.Services.Interface;

namespace App.Controllers.Admin
{
    public class TeacherController : BaseController
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeacherController> _logger;

        public TeacherController(ITeacherService teacherService, ILogger<TeacherController> logger)
        {
            _teacherService = teacherService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _teacherService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var teacher = await _teacherService.GetByIdAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TeacherCreateDto request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Request cannot be null" });
            }

            await _teacherService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), new { response = "Data successfully created" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] TeacherEditDto request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Request cannot be null" });
            }

            var existingTeacher = await _teacherService.GetByIdAsync(id);
            if (existingTeacher == null)
            {
                return NotFound();
            }

            await _teacherService.EditAsync(id, request);
            return Ok(new { response = "Data successfully updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingTeacher = await _teacherService.GetByIdAsync(id);
            if (existingTeacher == null)
            {
                return NotFound();
            }

            await _teacherService.DeleteAsync(id);
            return Ok(new { response = "Data successfully deleted" });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            var result = await _teacherService.SearchAsync(name);
            return Ok(result);
        }

        [HttpGet("sort")]
        public async Task<IActionResult> Sort([FromQuery] string sortBy)
        {
            var result = await _teacherService.SortAsync(sortBy);
            return Ok(result);
        }
    }
}
