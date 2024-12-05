using AutoMapper;
using BlogApi.Models;
using BlogApi.Repository.Implementations;
using BlogApi.Services.DTO;

namespace BlogApi.Services.Implementations
{
    public class TagService
    {
        private readonly TagRepository _tagRepository;
        private readonly IMapper _mapper;

        public TagService(TagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        // Get all tags
        public async Task<IEnumerable<TagDTO>> GetAllTagsAsync()
        {
            var result = await _tagRepository.GetAllTagsAsync();
            return _mapper.Map<IEnumerable<TagDTO>>(result);
        }

        // Get tag by ID
        public async Task<TagDTO?> GetTagByIdAsync(int id)
        {
            var result = await _tagRepository.GetTagByIdAsync(id);
            return _mapper.Map<TagDTO>(result);
        }

        // Add a new tag
        public async Task AddTagAsync(TagDTO tagDTO)
        {
            var mapping = _mapper.Map<Tag>(tagDTO);
            await _tagRepository.AddTagAsync(mapping);
        }

        // Update an existing tag
        public async Task UpdateTagAsync(TagDTO tagDTO)
        {
            var mapping = _mapper.Map<Tag>(tagDTO);
            await _tagRepository.UpdateTagAsync(mapping);
        }

        // Delete a tag (Soft Delete)
        public async Task DeleteTagAsync(int id)
        {
            var tag = await _tagRepository.GetTagByIdAsync(id);
            if (tag == null)
            {
                throw new KeyNotFoundException($"Tag with ID {id} not found.");
            }

            tag.IsDeleted = true;
            await _tagRepository.UpdateTagAsync(tag);
        }

        // Restore a deleted tag
        public async Task RestoreTagAsync(int id)
        {
            var tag = await _tagRepository.GetTagByIdAsync(id);
            if (tag == null)
            {
                throw new KeyNotFoundException($"Tag with ID {id} not found.");
            }

            tag.IsDeleted = false;
            await _tagRepository.UpdateTagAsync(tag);
        }
    }
}
