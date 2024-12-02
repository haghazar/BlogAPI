using BlogAPI.DTO;
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

        [HttpPost("api/add")]
        public async Task<IActionResult> AddPost([FromBody] AddPostRequest request)
        {
            if (request == null)
            {
                return BadRequest("Post is null.");
            }

            var post = new PostEntity
            {
                 Author = request.Author,
                 Title = request.Title,
                 Content = request.Content,

            };


            await _postService.AddPostAsync(post);
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(Guid id, [FromBody] UpdatePostRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request body is null.");
            }

            if (id == Guid.Empty)
            {
                return BadRequest("Invalid post ID.");
            }

            var existingPost = await _postService.GetPostByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound($"Post with ID {id} not found.");
            }

            existingPost.Author = request.Author ?? existingPost.Author;
            existingPost.Title = request.Title ?? existingPost.Title;
            existingPost.Content = request.Content ?? existingPost.Content;

            if (request.TagIds != null && request.TagIds.Any())
            {
                // Assuming a method in your service handles updating tags
                //await _postService.UpdateTagsForPostAsync(existingPost, request.TagIds);
            }

            await _postService.UpdatePostAsync(existingPost);
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
