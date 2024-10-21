using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WafiArche.Api.Controllers;
using WafiArche.Application.Products.Dtos;
using WafiArche.Application.PublicHolidays;
using WafiArche.Application.PublicHolidays.Dtos;
using WafiArche.Domain.PublicHolidays;

namespace WafiArche.Api.PublicHolidays
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicHolidayController : ControllerBase
    {
        private readonly IPublicHolidayService _publicHolidayService;
        private readonly ILogger<PublicHolidayController> _logger;

        public PublicHolidayController(IPublicHolidayService publicHolidayService, ILogger<PublicHolidayController> logger)
        {
            _publicHolidayService = publicHolidayService;
            _logger = logger;
        }

        [HttpGet]

        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<ActionResult<IEnumerable<PublicHolidayDto>>> GetAllPublicHoliday()
        {
            var publicHoliday = await _publicHolidayService.GetAllAsync();
            return Ok(publicHoliday);
        }


        [HttpGet("Id : int", Name = "GetPublicHolidayById")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<PublicHolidayDto>> GetPublicHolidayById(int id)
        {
            if (id == 0)
            {
                _logger.LogWarning("Invalid Public Holiday ID: {Id}", id);
                return BadRequest("Invalid Public Holiday ID.");
            }
            var publicHoliday = await _publicHolidayService.GetAsync(u => u.Id == id);

            if (publicHoliday == null)
            {
                _logger.LogWarning("Public Holiday with ID: {Id} not found.", id);
                return NotFound();
            }
            _logger.LogInformation("Returning Public Holiday with ID: {Id}", id);
            return Ok(publicHoliday);
        }

        [HttpPost]

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult<PublicHolidayDto>> CreatePublicHoliday([FromBody] PublicHolidayCreateDto publicHolidayCreateDto)
        {
            _logger.LogInformation("Received request to create a new Public Holiday.");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model validation failed for Public Holiday creation.");
                return BadRequest(ModelState);
            }

            if (await _publicHolidayService.GetAsync(u => u.Date == publicHolidayCreateDto.Date) != null)
            {
                _logger.LogWarning("Product creation failed: PublicHoliday Date '{PublicHoliday}' already exists.", publicHolidayCreateDto.Name);
                ModelState.AddModelError("Custom Error", "PublicHoliday Date already exists!");
                return BadRequest(ModelState);
            }
            if (publicHolidayCreateDto == null)
            {
                _logger.LogWarning("Public Holiday creation failed: The Public Holiday data is null.");
                return BadRequest();
            }

            var model = await _publicHolidayService.CreateAsync(publicHolidayCreateDto);
            _logger.LogInformation("Public Holiday created successfully with ID: {PublicHolidayId}", model.Id);
            return CreatedAtRoute("GetPublicHolidayById", new { id = model.Id }, model);
        }

        [HttpPut("Id : int", Name = "UpdatePublicHoliday")]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdatePublicHoliday(int id, [FromBody] PublicHolidayUpdateDto publicHolidayUpdateDto)
        {
            _logger.LogInformation("Received request to update public holiday with ID: {PublicHolidayId}", id);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid for public holiday update: {Errors}", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest(ModelState);
            }

            if (id != publicHolidayUpdateDto.Id || publicHolidayUpdateDto == null)
            {
                _logger.LogWarning("ID mismatch or null DTO for public holiday update. Expected ID: {ExpectedId}, Provided DTO ID: {ProvidedId}", id, publicHolidayUpdateDto?.Id);
                return BadRequest();
            }
            if (await _publicHolidayService.GetAsync(u => u.Date == publicHolidayUpdateDto.Date) != null)
            {
                _logger.LogWarning("Product creation failed: PublicHoliday Date '{PublicHoliday}' already exists.", publicHolidayUpdateDto.Name);
                ModelState.AddModelError("Custom Error", "PublicHoliday Date already exists!");
                return BadRequest(ModelState);
            }
            var result = await _publicHolidayService.UpdateAsync(publicHolidayUpdateDto);
            if (result == null)
            {
                _logger.LogWarning("Public holiday with ID: {PublicHolidayId} not found for update.", id);
                return NotFound();
            }
            _logger.LogInformation("Successfully updated public holiday with ID: {PublicHolidayId}", id);

            return NoContent();
        }



        [HttpDelete("Id : int", Name = "DeletePublicHoliday")]

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> DeletePublicHoliday(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var publicHoliday = await _publicHolidayService.GetAsync(u => u.Id == id);

            if (publicHoliday == null)
            {
                return NotFound();
            }

            var result = await _publicHolidayService.RemoveAsync(id);

            return NoContent();
        }
    }
}
