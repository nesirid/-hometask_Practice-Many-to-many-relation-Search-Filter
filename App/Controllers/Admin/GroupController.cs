using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.DTOs.Admin.Groups;
using Service.Services.Interface;

namespace App.Controllers.Admin
{
    public class GroupController : BaseController
    {
        private readonly IGroupService _groupService;
        private readonly ILogger<GroupController> _logger;

        public GroupController(IGroupService groupService, ILogger<GroupController> logger)
        {
            _groupService = groupService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _groupService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var country = await _groupService.GetByIdAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GroupCreateDto request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Request cannot be null" });
            }

            await _groupService.CreateAsync(request);
            return CreatedAtAction(nameof(Create), new { response = "Data successfully created" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] GroupEditDto request)
        {
            if (request == null)
            {
                return BadRequest(new { message = "Request cannot be null" });
            }

            var existingCountry = await _groupService.GetByIdAsync(id);
            if (existingCountry == null)
            {
                return NotFound();
            }

            await _groupService.EditAsync(id, request);
            return Ok(new { response = "Data successfully updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingCountry = await _groupService.GetByIdAsync(id);
            if (existingCountry == null)
            {
                return NotFound();
            }

            await _groupService.DeleteAsync(id);
            return Ok(new { response = "Data successfully deleted" });
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery] string name, [FromQuery] int? capacity)
        {
            var result = await _groupService.FilterAsync(name, capacity);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string name, [FromQuery] string surname, [FromQuery] int? age = null)
        {
            var result = await _groupService.SearchAsync(name, surname, age);
            return Ok(result);
        }

        [HttpGet("sort")]
        public async Task<IActionResult> Sort([FromQuery] string sortBy)
        {
            var result = await _groupService.SortAsync(sortBy);
            return Ok(result);
        }
    }
}
