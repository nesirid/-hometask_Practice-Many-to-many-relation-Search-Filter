using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Educations;
using Service.Services.Interface;

namespace App.Controllers.Admin
{
    public class EducationController : BaseController
    {
        private readonly IEducationService _educationService;

        public EducationController(IEducationService educationService)
        {
            _educationService = educationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _educationService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var education = await _educationService.GetByIdAsync(id);
            if (education == null)
            {
                return NotFound();
            }
            return Ok(education);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EducationCreateDto request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Request cannot be null" });
            }

            try
            {
                await _educationService.CreateAsync(request);
                return CreatedAtAction(nameof(Create), new { response = "Data successfully created" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] EducationEditDto request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Request cannot be null" });
            }

            var existingEducation = await _educationService.GetByIdAsync(id);
            if (existingEducation == null)
            {
                return NotFound();
            }

            await _educationService.EditAsync(id, request);
            return Ok(new { response = "Data successfully updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingEducation = await _educationService.GetByIdAsync(id);
            if (existingEducation == null)
            {
                return NotFound();
            }

            await _educationService.DeleteAsync(id);
            return Ok(new { response = "Data successfully deleted" });
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name)
        {
            var educations = await _educationService.SearchAsync(name);
            return Ok(educations);
        }

        [HttpGet("sorted")]
        public async Task<IActionResult> GetAllSortedByName()
        {
            var educations = await _educationService.GetAllSortedByNameAsync();
            return Ok(educations);
        }
    }
}
