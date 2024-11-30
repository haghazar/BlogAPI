using BlogAPI.Models;
using BlogAPI.Repositories;

namespace BlogAPI.Service
{
    public class TagService
    {
        private readonly TagRepository _tagRepository;

        public TagService(TagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        // Get all tags
        public async Task<IEnumerable<TagEntity>> GetAllTagsAsync()
        {
            return await _tagRepository.GetAllTagsAsync();
        }

        // Get tag by ID
        public async Task<TagEntity?> GetTagByIdAsync(Guid id)
        {
            return await _tagRepository.GetTagByIdAsync(id);
        }

        // Add a new tag
        public async Task AddTagAsync(TagEntity tag)
        {
            // Business logic before adding the tag
            await _tagRepository.AddTagAsync(tag);
        }

        // Update an existing tag
        public async Task UpdateTagAsync(TagEntity tag)
        {
            // Business logic before updating the tag
            await _tagRepository.UpdateTagAsync(tag);
        }

        // Delete a tag
        public async Task DeleteTagAsync(Guid id)
        {
            // You can implement additional logic (e.g., check if the tag is associated with posts)
            await _tagRepository.DeleteTagAsync(id);
        }

    }
}
