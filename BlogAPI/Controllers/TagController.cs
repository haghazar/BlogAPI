using BlogApi.Services.DTO;
using BlogApi.Services.Implementations;
using BlogAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : ControllerBase
    {
        private readonly TagService _tagService;

        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTags()
        {
            var tags = await _tagService.GetAllTagsAsync();
            return Ok(tags);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTagById(int id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            if (tag == null)
                return NotFound($"Tag with ID {id} not found.");

            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> AddTag([FromBody] AddTagRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Invalid tag data.");
            }

            try
            {
                var tag = new TagDTO
                {
                    Name = request.Name
                };

                await _tagService.AddTagAsync(tag);
                return CreatedAtAction(nameof(GetTagById), new { id = tag.Id }, tag);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(int id, [FromBody] AddTagRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return BadRequest("Invalid tag data.");
            }

            var existingTag = await _tagService.GetTagByIdAsync(id);
            if (existingTag == null)
            {
                return NotFound($"Tag with ID {id} not found.");
            }

            existingTag.Name = request.Name ?? existingTag.Name;

            await _tagService.UpdateTagAsync(existingTag);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var existingTag = await _tagService.GetTagByIdAsync(id);
            if (existingTag == null)
            {
                return NotFound($"Tag with ID {id} not found.");
            }

            await _tagService.DeleteTagAsync(id);
            return NoContent();
        }
    }
}
