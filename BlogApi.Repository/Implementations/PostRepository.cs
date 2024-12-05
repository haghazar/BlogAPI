using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repository.Implementations
{
    public class PostRepository
    {
        private readonly BlogDbContext _dbContext;

        public PostRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _dbContext.Posts
                .Include(p => p.Tags)
                .Where(x => !x.IsDeleted)
                .ToListAsync();
        }
        public async Task<IEnumerable<Post>> GetIsDeletedPostsAsync()
        {
            return await _dbContext.Posts
                .Include(p => p.Tags)
                .Where(x => x.IsDeleted)
                .ToListAsync();
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _dbContext.Posts
                
                .AsTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            
        }

        public async Task AddPostAsync(Post post)
        {
            _dbContext.Posts.Add(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }


        public async Task DeletePostAsync(int id)
        {
            var post = await GetPostByIdAsync(id);
            if (post != null)
            {
                _dbContext.Posts.Remove(post);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
