using BlogAPI.DTO;
using BlogAPI.Models;
using BlogAPI.Service;
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
        public async Task<IActionResult> GetTagById(Guid id)
        {
            var tag = await _tagService.GetTagByIdAsync(id);
            if (tag == null)
                return NotFound();

            return Ok(tag);
        }

        [HttpPost]
        public async Task<IActionResult> AddTag([FromBody] AddTagRequest request)
        {
            try
            {
                var tag = new TagEntity
                {
                    Name = request.Name,
                    PostTags = []
                };

                await _tagService.AddTagAsync(tag);
                return Ok();
            } catch (Exception ex)
            {
                throw new Exception($"{ex.Message} {ex.InnerException?.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTag(Guid id, [FromBody] AddTagRequest request)
        {
            if (id == null)
                return BadRequest("not found id");
            var existingTag = await _tagService.GetTagByIdAsync(id);

            existingTag.Name = request.Name ?? existingTag.Name;
            
            await _tagService.UpdateTagAsync(existingTag);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(Guid id)
        {
            await _tagService.DeleteTagAsync(id);
            return NoContent();
        }
    }
}
