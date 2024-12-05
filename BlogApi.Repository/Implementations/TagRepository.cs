using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Repository.Implementations
{

    public class TagRepository
    {
        private readonly BlogDbContext _dbContext;

        public TagRepository(BlogDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Tag>> GetAllTagsAsync()
        {
            return await _dbContext.Tags
                .Include(t => t.Posts)
                .ToListAsync();
        }

        public async Task<Tag?> GetTagByIdAsync(int id)
        {
            return await _dbContext.Tags
                .Include(t => t.Posts)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTagAsync(Tag tag)
        {
            _dbContext.Tags.Add(tag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateTagAsync(Tag tag)
        {
            _dbContext.Tags.Update(tag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteTagAsync(int id)
        {
            var tag = await GetTagByIdAsync(id);
            if (tag != null)
            {
                _dbContext.Tags.Remove(tag);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
