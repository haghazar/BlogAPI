
using AutoMapper;
using BlogApi.Models;
using BlogApi.Repository.Implementations;
using BlogApi.Services.DTO;

namespace BlogApi.Services.Implementations
{
    public class PostService
    {
        private readonly PostRepository _PostDTORepository;

        private readonly IMapper _mapper;


        public PostService(PostRepository PostDTORepository,IMapper mapper)
        {
            _PostDTORepository = PostDTORepository;
            _mapper = mapper;
        }

        // Get all PostDTOs
        public async Task<IEnumerable<PostDTO>> GetAllPostsAsync()
        {
            var result = await _PostDTORepository.GetAllPostsAsync();
            return _mapper.Map<IEnumerable<PostDTO>>(result);
        }
        public async Task<IEnumerable<PostDTO>> GetIsDeletedPostsAsync()
        {
            var result = await _PostDTORepository.GetIsDeletedPostsAsync();
            return _mapper.Map<IEnumerable<PostDTO>>(result);
        }

        // Get PostDTO by ID
        public async Task<PostDTO?> GetPostByIdAsync(int id)
        {
            var result = await _PostDTORepository.GetPostByIdAsync(id);
            return _mapper.Map<PostDTO>(result);
        }

        // Add a new PostDTO
        public async Task AddPostAsync(PostDTO PostDTO)
        {

            var mapping = _mapper.Map<Post>(PostDTO);
            await _PostDTORepository.AddPostAsync(mapping);
            //return _mapper.Map<PostDTO>(result);
        }

        // Update an existing PostDTO
        public async Task UpdatePostAsync(PostDTO PostDTO)
        {
            
            var mapping = _mapper.Map<Post>(PostDTO);
            await _PostDTORepository.UpdatePostAsync(mapping);
        }

       
        public async Task DeletePostAsync(int id)
        {
            var PostDTO = await _PostDTORepository.GetPostByIdAsync(id);
            if (PostDTO == null)
            {
                throw new KeyNotFoundException($"PostDTO with ID {id} not found.");
            }

            // Mark as soft-deleted
            PostDTO.IsDeleted = true;
            //PostDTO.DeletedOnUtc = DateTime.UtcNow;

            // Save changes
            await _PostDTORepository.UpdatePostAsync(PostDTO);
        }
        public async Task RestorePostAsync(int id)
        {
            var PostDTO = await _PostDTORepository.GetPostByIdAsync(id);
            if (PostDTO == null)
            {
                throw new KeyNotFoundException($"PostDTO with ID {id} not found.");
            }

            PostDTO.IsDeleted = false;
            //PostDTO.DeletedOnUtc = null;
            await _PostDTORepository.UpdatePostAsync(PostDTO);
        }
    }
}
