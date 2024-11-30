using BlogAPI.Models;
using BlogAPI.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BlogAPI.Service
{
    public class PostService
    {
        private readonly PostRepository _postRepository;

        public PostService(PostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // Get all posts
        public async Task<IEnumerable<PostEntity>> GetAllPostsAsync()
        {
            return await _postRepository.GetAllPostsAsync();
        }

        // Get post by ID
        public async Task<PostEntity?> GetPostByIdAsync(Guid id)
        {
            return await _postRepository.GetPostByIdAsync(id);
        }

        // Add a new post
        public async Task AddPostAsync(PostEntity post)
        {
            // Business logic before adding the post (e.g. validation, data enrichment)

            await _postRepository.AddPostAsync(post);
        }

        // Update an existing post
        public async Task UpdatePostAsync(PostEntity post)
        {
            // Business logic before updating the post
            await _postRepository.UpdatePostAsync(post);
        }

        // Delete a post
        public async Task DeletePostAsync(Guid id)
        {
            // You can implement additional logic (e.g., check if a post has tags, etc.)
            await _postRepository.DeletePostAsync(id);
        }
    }
}
