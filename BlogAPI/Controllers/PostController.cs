using BlogAPI.Models;
using BlogAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;

        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] PostEntity post)
        {
            if (post == null)
            {
                return BadRequest("Post is null.");
            }

            // Ensure that PostTags are not null and contain valid PostId and TagId
            if (post.PostTags == null || !post.PostTags.Any())
            {
                return BadRequest("Post must have at least one tag.");
            }

            foreach (var postTag in post.PostTags)
            {
                if (postTag.PostId == Guid.Empty || postTag.TagId == Guid.Empty)
                {
                    return BadRequest("Both PostId and TagId must be valid.");
                }
            }

            await _postService.AddPostAsync(post);
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] PostEntity post)
        {
            if (id != post.Id)
                return BadRequest("ID mismatch");

            await _postService.UpdatePostAsync(post);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }
    }
}
