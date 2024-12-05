using BlogApi.Services.DTO;
using BlogApi.Services.Implementations;
using BlogAPI.DTO;
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
        [HttpGet("get-isdeleted-posts")]
        public async Task<IActionResult> GetIsDeletedPosts()
        {
            var posts = await _postService.GetIsDeletedPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        [HttpPost("add-posts")]
        public async Task<IActionResult> AddPost([FromBody] PostDTO request)
        {
            if (request == null)
            {
                return BadRequest("Post is null.");
            }

           

           
            await _postService.AddPostAsync(request);

           
            return CreatedAtAction(nameof(GetPostById), new { id = request.Id }, request);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] UpdatePostRequest request)
        {
            if (request == null)
            {
                return BadRequest("Request body is null.");
            }

            //if (id == Empty)
            //{
            //    return BadRequest("Invalid post ID.");
            //}

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
                //await _postService.UpdateTagsForPostAsync(existingPost, request.TagIds);
            }

            await _postService.UpdatePostAsync(existingPost);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.DeletePostAsync(id);
            return NoContent();
        }

        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestorePost(int id)
        {
            try
            {
                await _postService.RestorePostAsync(id);
                return Ok($"Post with ID {id} has been restored.");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }


    }
}
